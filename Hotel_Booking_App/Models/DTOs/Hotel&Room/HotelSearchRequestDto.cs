public class HotelSearchRequestDto
{
    public string? City { get; set; }
    public string? Country { get; set; }
    public int? MinStarRating { get; set; }
    public int? MaxStarRating { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public bool? OnlyAvailableRooms { get; set; } // Filter only available rooms
}
