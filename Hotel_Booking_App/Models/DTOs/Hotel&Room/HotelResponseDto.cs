namespace Hotel_Booking_App.Models.DTOs.Hotel_Room
{
    public class HotelResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int TotalRooms { get; set; }
        public decimal Rating { get; set; }
        public int HotelOwnerId { get; set; }
        public List<RoomResponseDto> Rooms { get; set; }
    }
}
