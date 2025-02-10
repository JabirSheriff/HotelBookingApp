using Hotel_Booking_App.Models;

namespace Hotel_Booking_App.Interface.Hotel_Room
{
    public interface IHotelRepository
    {
        Task AddHotelAsync(Hotel hotel);
        Task<IEnumerable<Hotel>> GetHotelsByOwnerIdAsync(int ownerId);
        Task<Hotel?> GetHotelByIdAsync(int hotelId);
        Task UpdateHotelAsync(Hotel hotel);
        Task DeleteHotelAsync(int hotelId);
        Task<bool> SaveChangesAsync();
    }
}
