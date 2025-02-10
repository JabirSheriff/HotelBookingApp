using Hotel_Booking_App.Contexts;
using Hotel_Booking_App.Interface.Hotel_Room;
using Hotel_Booking_App.Models;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Booking_App.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelBookingDbContext _context;

        public RoomRepository(HotelBookingDbContext context)
        {
            _context = context;
        }

        public async Task AddRoomAsync(Room room)
        {
            await _context.Rooms.AddAsync(room);
        }

        public async Task<IEnumerable<Room>> GetRoomsByHotelIdAsync(int hotelId)
        {
            return await _context.Rooms.Where(r => r.HotelId == hotelId).ToListAsync();
        }

        public async Task<Room?> GetRoomByIdAsync(int roomId)
        {
            return await _context.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);
        }

        public async Task UpdateRoomAsync(Room room)
        {
            _context.Rooms.Update(room);
        }

        public async Task DeleteRoomAsync(int roomId)
        {
            var room = await GetRoomByIdAsync(roomId);
            if (room != null)
                _context.Rooms.Remove(room);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
