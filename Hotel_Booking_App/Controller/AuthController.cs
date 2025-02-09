using Hotel_Booking_App.Interface;
using Hotel_Booking_App.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Booking_App.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;

        public AuthController(IAuthService authService, IUserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        [AllowAnonymous] // ✅ Explicitly allow registration for all users
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto userregistrationDto)
        {
            try
            {
                var userResponse = await _authService.RegisterAsync(userregistrationDto);
                return Ok(userResponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Register: {ex}");
                return BadRequest(new { error = "Registration failed. Please try again later." });
            }
        }

        [HttpPost("login")]
        [AllowAnonymous] // ✅ Explicitly allow login for all users
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var response = await _authService.LoginAsync(loginDto);

                if (response == null)
                    return Unauthorized(new { error = "Invalid email or password" }); // ✅ Use Unauthorized for incorrect login

                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Login: {ex}"); // ✅ Log full exception details
                return BadRequest(new { error = "Login failed. Please try again later." });
            }
        }
    }
}
