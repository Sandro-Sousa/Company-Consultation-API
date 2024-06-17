using NSubstitute;
using Service.Interfaces;
using Repository.Interfaces;
using Service.Services;
using Entities.Entites;
using Service.DTOs;


namespace Company_Consultation.Tests.Services
{
    public class LoginServiceTests
    {
        private readonly ILoginRepository _loginRepository = Substitute.For<ILoginRepository>();
        private readonly IEmailConfigurationService _emailService = Substitute.For<IEmailConfigurationService>();
        private readonly LoginService _loginService;

        public LoginServiceTests()
        {
            _loginService = new LoginService(_loginRepository, _emailService);
        }


        [Fact]
        public async Task CreateUser_ShouldReturnSuccess_WhenUserIsCreated()
        {
            // Arrange
            var userDTO = new UserDTO { Name = "teste" , Email = "teste@hotmail.com", Password = "12345678"};
            _loginRepository.EmailExists(Arg.Any<string>()).Returns(Task.FromResult(false));

            // Act
            var result = await _loginService.CreateUser(userDTO);

            // Assert
            Assert.Equal("Conta criada", result.Message);
            await _loginRepository.Received(1).SaveUser(Arg.Any<User>());
            await _emailService.Received(1).SendVerificationEmail(Arg.Any<User>());
        }

    }
}
