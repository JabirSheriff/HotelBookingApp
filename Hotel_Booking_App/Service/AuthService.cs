using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Hotel_Booking_App.Interface;
using Hotel_Booking_App.Models.DTOs;
using Hotel_Booking_App.Models;
using Microsoft.IdentityModel.Tokens;

namespace Hotel_Booking_App.Service
{
    public class AuthService :IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<UserRegistrationResponseDto> RegisterAsync(UserRegistrationDto userregistrationDto)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(userregistrationDto.Email);
            if (existingUser != null)
                throw new Exception("User already exists.");

            using var hmac = new HMACSHA512();
            var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userregistrationDto.Password));
            var passwordSalt = hmac.Key;

            var user = new User
            {
                FullName = userregistrationDto.FullName,
                Email = userregistrationDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                PhoneNumber = userregistrationDto.PhoneNumber,
                Role = "Unassigned" // Default role
            };

            await _userRepository.AddUserAsync(user);
            await _userRepository.SaveChangesAsync();

            return new UserRegistrationResponseDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                PhoneNumber = user.PhoneNumber,
            };
        }

        public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetUserByEmailAsync(loginDto.Email);
            if (user == null)
                throw new Exception("User not found.");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            if (!computedHash.SequenceEqual(user.PasswordHash))
                throw new Exception("Invalid password.");

            // Check if the user's role is assigned
            if (user.Role == "Unassigned")
            {
                return new LoginResponseDto
                {

                    Message = "Login successful, but role is not assigned. Please contact Admin.",
                    Token = null,
                    FullName = user.FullName,
                    Email = user.Email,
                    Role = user.Role
                };
            }

            // ✅ Fetch HotelOwner ID
            int? hotelOwnerId = null;
            if (user.Role == "HotelOwner")
            {
                var hotelOwner = await _userRepository.GetHotelOwnerByUserIdAsync(user.Id);
                hotelOwnerId = hotelOwner?.Id;  // Get HotelOwner ID
            }

            // 🔥 Ensure JWT Secret Key is loaded properly
            var secretKey = _configuration["Jwt:Secret"];
            if (string.IsNullOrEmpty(secretKey))
                throw new Exception("JWT Secret Key is missing in configuration.");

            var key = Encoding.UTF8.GetBytes(secretKey);

            // ✅ Add Role, Email, and ID claims properly
            var claims = new List<Claim>
            {
                new Claim("nameId", user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            if (hotelOwnerId.HasValue)
                claims.Add(new Claim("hotelOwnerId", hotelOwnerId.Value.ToString()));

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            string jwtToken = tokenHandler.WriteToken(token);

            // ✅ Ensure Role-Based Messages in Response
            string welcomeMessage = user.Role switch
            {
                "Admin" => "Welcome to Admin Dashboard",
                "Customer" => "Welcome to Hotel Booking App",
                "HotelOwner" => "Welcome to Hotel Booking Dashboard",
                _ => "Unauthorized access"
            };

            return new LoginResponseDto
            {
                FullName = user.FullName,  // ✅ Send Full Name
                Email = user.Email,  // ✅ Send Email
                Role = user.Role,
                HotelOwnerId = hotelOwnerId,
                Message = welcomeMessage,
                Token = jwtToken
            };
        }

        public async Task<UserRegistrationResponseDto> RegisterCustomerAsync(UserRegistrationDto userRegistrationDto)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(userRegistrationDto.Email);
            if (existingUser != null)
                throw new Exception("User already exists.");

            using var hmac = new HMACSHA512();
            var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userRegistrationDto.Password));
            var passwordSalt = hmac.Key;

            var user = new User
            {
                FullName = userRegistrationDto.FullName,
                Email = userRegistrationDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                PhoneNumber = userRegistrationDto.PhoneNumber,
                Role = "Customer"  // Automatically set role to Customer
            };

            await _userRepository.AddUserAsync(user);
            await _userRepository.SaveChangesAsync();

            var customer = new Customer
            {
                UserId = user.Id,  // ✅ Associate customer with the User
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };

            await _userRepository.AddCustomerAsync(customer); // ✅ Save customer details
            await _userRepository.SaveChangesAsync();

            return new UserRegistrationResponseDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                PhoneNumber = user.PhoneNumber,
            };
        }

        public async Task<bool> UpdateUserProfileAsync(int userId, UpdateUserDto updateUserDto)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) return false;

            // Update only non-null values
            if (!string.IsNullOrEmpty(updateUserDto.FullName))
                user.FullName = updateUserDto.FullName;

            if (!string.IsNullOrEmpty(updateUserDto.Email))
                user.Email = updateUserDto.Email;

            if (!string.IsNullOrEmpty(updateUserDto.PhoneNumber))
                user.PhoneNumber = updateUserDto.PhoneNumber;

            if (!string.IsNullOrEmpty(updateUserDto.Password))
            {
                using var hmac = new HMACSHA512();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(updateUserDto.Password));
                user.PasswordSalt = hmac.Key;
            }

            await _userRepository.SaveChangesAsync();
            return true;
        }


    }
}
