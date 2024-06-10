
using Service.DTOs;

namespace Service.Interfaces
{
    public interface ILoginService
    {
        Task<Service.Results.Create.Response> CreateUser(UserDTO userDTO);
        Task<Service.Results.Authenticate.Response> AuthenticateUser(string email, string password);

        Task<Service.Results.VerfifyCode.Response> VerifyCode(string email, string code);
    }
}
