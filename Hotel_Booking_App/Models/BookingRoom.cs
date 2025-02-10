using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel_Booking_App.Models
{
    public class BookingRoom
    {
        [Key]
        public int Id { get; set; } // ✅ Added explicit primary key

        // ✅ Fixed Foreign Key for Booking
        [Required]
        [ForeignKey("Booking")]
        public int BookingId { get; set; }
        public Booking Booking { get; set; }

        // ✅ Fixed Foreign Key for Room
        [Required]
        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
