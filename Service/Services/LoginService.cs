using Entities.Entites;
using Entities.ValueObjects;
using Entities;
using Repository.Interfaces;
using Service.DTOs;
using Service.Interfaces;
using Service.Results.Authenticate;
using Company_Consultation.WebAPI.Extensions;

namespace Service.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IEmailConfigurationService _emailService;

        public LoginService(ILoginRepository loginRepository, IEmailConfigurationService emailService)
        {
            _loginRepository = loginRepository;
            _emailService = emailService;
        }

        public async Task<Service.Results.Create.Response> CreateUser(UserDTO userDTO)
        {
            try
            {
                var res = Specification.Ensure(userDTO.Email, userDTO.Password);
                if (!res.IsValid)
                {
                    var notificationMessages = string.Join("; ", res.Notifications.Select(n => n.Message));
                    return new Service.Results.Create.Response(notificationMessages, 400, res.Notifications);
                }
            }
            catch (Exception)
            {
                return new Service.Results.Create.Response("Não foi possível validar sua requisição", 500);
            }

            Email email;
            Password password;
            User user;
            try
            {
                email = new Email(userDTO.Email);
                password = new Password(userDTO.Password);
                user = new User(userDTO.Name, email, password);
            }
            catch (Exception ex)
            {
                return new Service.Results.Create.Response(ex.Message, 400);
            }

            try
            {
                var exists = await _loginRepository.EmailExists(email);
                if (exists)
                {
                    return new Service.Results.Create.Response("Esse Usuário já existe!", 400);
                }
            }
            catch
            {
                return new Service.Results.Create.Response("Erro ao verificar usuário", 500);
            }

            try
            {
                await _loginRepository.SaveUser(user);
            }
            catch
            {
                return new Service.Results.Create.Response("Erro ao salvar usuário", 500);
            }

            try
            {
                await _emailService.SendVerificationEmail(user);
            }
            catch (Exception ex)
            {
                return new Service.Results.Create.Response(ex.Message, 500);
            }

            return new Service.Results.Create.Response("Conta criada", new Service.Results.Create.ResponseData(user.Id, user.Name, user.Email));
        }

        public async Task<Service.Results.Authenticate.Response> AuthenticateUser(string email, string password)
        {
            try
            {
                var res = Specification.Ensure(email, password);
                if (!res.IsValid)
                {
                    var notificationMessages = string.Join("; ", res.Notifications.Select(n => n.Message));
                    return new Service.Results.Authenticate.Response(notificationMessages, 400, res.Notifications);
                }
            }
            catch (Exception)
            {
                return new Service.Results.Authenticate.Response("Não foi possível validar sua requisição", 500);
            }

            User? user;
            try
            {
                user = await _loginRepository.GetUserByEmail(email);
                if (user is null || !user.Password.Challenge(password))
                {
                    return new Service.Results.Authenticate.Response("Usuário ou senha inválidos", 400);
                }
            }
            catch
            {
                return new Service.Results.Authenticate.Response("Erro ao obter usuário", 500);
            }

            try
            {
                if (!user.Email.Verification.IsActive)
                {
                    return new Service.Results.Authenticate.Response("Conta inativa", 400);
                }
            }
            catch (Exception)
            {
                return new Service.Results.Authenticate.Response("Erro ao verificar status de verificação", 500);
            }

            try
            {
                var data = new ResponseData
                {
                    Id = user.Id.ToString(),
                    Name = user.Name,
                    Email = user.Email.Address
                };

                data.Token = JwtExtension.Generate(data);

                return new Service.Results.Authenticate.Response("Login efetuado", data);
            }
            catch
            {
                return new Service.Results.Authenticate.Response("Erro ao criar token", 500);
            }
        }


        public async Task<Service.Results.VerfifyCode.Response> VerifyCode(string email, string code)
        {
            try
            {
                var res = Specification.EnsureCode(code);
                if (!res.IsValid)
                {
                    var notificationMessages = string.Join("; ", res.Notifications.Select(n => n.Message));
                    return new Service.Results.VerfifyCode.Response(notificationMessages, 400, res.Notifications);
                }

                var user = await _loginRepository.GetUserByEmail(email);
                if (user is null)
                {
                    return new Service.Results.VerfifyCode.Response("Perfil não encontrado", 404);
                }

                if (user.Email.Verification.Code != code)
                {
                    return new Service.Results.VerfifyCode.Response("Código inválido", 400);
                }

                var result = user.Email.Verification.VerifyCode(code);
                if (!string.IsNullOrEmpty(result.ErrorValidateCode))
                {
                    return new Service.Results.VerfifyCode.Response(result.ErrorValidateCode, 400);
                }

                await _loginRepository.UpdateUser(user);
            }
            catch (Exception)
            {
                // Para debug, pode-se logar a exceção ex.Message aqui
                return new Service.Results.VerfifyCode.Response("Não foi possível validar sua requisição", 500);
            }

            return new Service.Results.VerfifyCode.Response("Conta verificada", 200);
        }
    }
}
