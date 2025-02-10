namespace Hotel_Booking_App.Models.DTOs
{
    public class LoginResponseDto
    {
        public string FullName { get; set; }  // ✅ Add Full Name
        public string Email { get; set; }  // ✅ Add Email
        public string Role { get; set; }  // ✅ Add Role
        public string Message { get; set; }
        public string Token { get; set; }
        public int? HotelOwnerId { get; set; }
    }
}
