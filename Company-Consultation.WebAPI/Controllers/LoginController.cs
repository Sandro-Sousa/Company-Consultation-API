using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Company_Consultation.WebAPI.Controllers
{
    [ApiController]
    [Route("api/Login")]
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("v1/createUser")]
        [SwaggerResponse(StatusCodes.Status200OK, "Inserted Successfully", typeof(UserDTO))]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Incorrect Header Data", typeof(string))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Server Error", typeof(string))]
        public async Task<IActionResult> RegisterLogin([FromBody] UserDTO userDTO)
        {
            try
            {
                var result = await _loginService.CreateUser(userDTO);
                if (!result.IsSuccess)
                {
                    return NotFound(result.Message);
                }

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("v1/authenticate")]
        [SwaggerResponse(StatusCodes.Status200OK, "Login Successfully", typeof(UserLoginDTO))]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Incorrect Header Data", typeof(string))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Server Error", typeof(string))]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO userLoginDTO)
        {
            try
            {
                var result = await _loginService.AuthenticateUser(userLoginDTO.Email, userLoginDTO.Password);
                if (!result.IsSuccess)
                {
                    return NotFound(result.Message);
                }

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("v1/verifyCode")]
        [SwaggerResponse(StatusCodes.Status200OK, "Verified Successfully", typeof(ValidateConfirmationCodeDTO))]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Incorrect Header Data", typeof(string))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Server Error", typeof(string))]
        public async Task<IActionResult> Verify([FromBody] ValidateConfirmationCodeDTO validateConfirmationCodeDTO)
        {
            try
            {
                var result = await _loginService.VerifyCode(validateConfirmationCodeDTO.Email, validateConfirmationCodeDTO.Code);
                if (!result.IsSuccess)
                {
                    return NotFound(result.Message);
                }

                return Ok(result.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
