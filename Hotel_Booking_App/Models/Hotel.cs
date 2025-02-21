using Hotel_Booking_App.Models;
using HotelBookingApp.Models;
using System.ComponentModel.DataAnnotations;

public class Hotel
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }

    [Range(1, 5, ErrorMessage = "Star Rating must be between 1 and 5")]
    public int? StarRating { get; set; }
    public string Description { get; set; }

    [Required]
    public int HotelOwnerId { get; set; }  // ✅ Added foreign key
    public HotelOwner HotelOwner { get; set; }

    public string? ContactEmail { get; set; }
    public string? ContactPhone { get; set; }
    public bool? IsActive { get; set; }

    //// ✅ Instead of List<string>, store Amenities as a comma-separated string or separate table
    //public List<string>? Amenities { get; set; }

    // ✅ Navigation Properties
    public ICollection<Room> Rooms { get; set; } = new List<Room>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

}
