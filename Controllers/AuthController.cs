using Microsoft.AspNetCore.Mvc;
using series_dotnet.Data;
using System.Threading.Tasks;
using series_dotnet.Dtos.User;
using series_dotnet.Models;
using series_dotnet.Services.EmailService;

namespace series_dotnet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        private readonly IEmailService _emailService;

        public AuthController(IAuthRepository authRepository, IEmailService emailService) 
        {
            _authRepository = authRepository;
            _emailService = emailService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDto request)
        {
            ServiceResponse<int> response = await _authRepository.Register(
                new User { Username = request.Username, Email = request.Email }, request.Password
            );
            if (!response.Success) {
                return BadRequest(response);
            }

            var message = new Message(new string[] { request.Email }, "Test email", "This is the content from our email.");
            await _emailService.SendMailAsync(message);
            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto request)
        {
            ServiceResponse<string> response = await _authRepository.Login(
                request.Username, request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}