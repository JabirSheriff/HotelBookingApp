using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel_Booking_App.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string RoomNumber { get; set; }

        [Required]
        public RoomType Type { get; set; } // ✅ Changed to Enum for structured room types

        [Required]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PricePerNight { get; set; }

        [Required]
        public bool IsAvailable { get; set; } = true; // ✅ Default available

        [Required]
        public int Capacity { get; set; }
        public int RoomTypeId { get; set; }


        // ✅ Fixed Foreign Key for Hotel
        [Required]
        [ForeignKey("Hotel")]
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }

        // ✅ Many-to-Many Relationship with Booking
        public ICollection<BookingRoom> BookingRooms { get; set; } = new List<BookingRoom>();
        
    }

    public enum RoomType
    {
        Single = 1,
        Double = 2,
        Suite = 3,
        Deluxe = 4
    }
}
