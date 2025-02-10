namespace Hotel_Booking_App.Models.DTOs.Hotel_Room
{
    public class AddRoomDto
    {
        public int HotelId { get; set; }
        public string RoomType { get; set; }
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; }
        public string Amenities { get; set; }
    }
}
