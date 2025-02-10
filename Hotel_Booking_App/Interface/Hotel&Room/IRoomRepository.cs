using System;
using Hotel_Booking_App.Models;

namespace Hotel_Booking_App.Interface.Hotel_Room
{
    public interface IRoomRepository
    {
        Task AddRoomAsync(Room room);
        Task<Room?> GetRoomByIdAsync(int roomId);
        Task<IEnumerable<Room>> GetRoomsByHotelIdAsync(int hotelId);
        Task<bool> UpdateRoomAsync(Room room);
        Task<bool> DeleteRoomAsync(Room room);

        Task<bool> SaveChangesAsync();
    }

}
