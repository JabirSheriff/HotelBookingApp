using Hotel_Booking_App.Models;

namespace Hotel_Booking_App.Interface.Hotel_Room
{
    public interface IRoomRepository
    {
        Task AddRoomAsync(Room room);
        Task<IEnumerable<Room>> GetRoomsByHotelIdAsync(int hotelId);
        Task<Room?> GetRoomByIdAsync(int roomId);
        Task UpdateRoomAsync(Room room);
        Task DeleteRoomAsync(int roomId);
        Task<bool> SaveChangesAsync();
    }
}
