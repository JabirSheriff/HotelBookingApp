using System.ComponentModel.DataAnnotations;
using Hotel_Booking_App.Models;

namespace Hotel_Booking_App.Models.DTOs.Booking
{
    public class BookingRequestDto
    {
        [Required]
        public int HotelId { get; set; }

        [Required]
        public RoomType RoomType { get; set; }  // Specify the type of room

        [Required]
        [Range(1, 10, ErrorMessage = "You can book between 1 and 10 rooms.")]
        public int NumberOfRooms { get; set; } // Number of rooms needed

        [Required]
        [Range(1, 10, ErrorMessage = "Number of guests must be between 1 and 10.")]
        public int NumberOfGuests { get; set; }

        [Required]
        public DateTime CheckInDate { get; set; }

        [Required]
        public DateTime CheckOutDate { get; set; }

        public string? SpecialRequest { get; set; }
    }
}
