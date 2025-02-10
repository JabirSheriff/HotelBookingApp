namespace Hotel_Booking_App.Models.DTOs.Hotel_Room
{
    public class AddHotelDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public int HotelOwnerId { get; set; }   
    }
}
