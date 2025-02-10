using System.Security.Claims;
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

        [HttpPost("register-customer")]
        [AllowAnonymous] // ✅ Allow open registration for customers
        public async Task<IActionResult> RegisterCustomer([FromBody] UserRegistrationDto userRegistrationDto)
        {
            try
            {
                var userResponse = await _authService.RegisterCustomerAsync(userRegistrationDto);
                return Ok(userResponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Register Customer: {ex}");
                return BadRequest(new { error = "Customer registration failed. Please try again later." });
            }
        }

        [Authorize]
        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserDto updateUserDto)
        {
            try
            {
                // Get logged-in user's ID from JWT token
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var result = await _authService.UpdateUserProfileAsync(userId, updateUserDto);
                if (!result) return BadRequest(new { error = "Profile update failed." });

                return Ok(new { message = "Profile updated successfully." });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] UpdateProfile: {ex}");
                return StatusCode(500, new { error = "An error occurred while updating profile." });
            }
        }


    }
}
