using Hotel_Booking_App.Contexts;
using Hotel_Booking_App.Interface.Hotel_Room;
using Hotel_Booking_App.Models;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Booking_App.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly HotelBookingDbContext _context;

        public HotelRepository(HotelBookingDbContext context)
        {
            _context = context;
        }

        public async Task AddHotelAsync(Hotel hotel)
        {
            await _context.Hotels.AddAsync(hotel);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Hotel>> GetHotelsByOwnerIdAsync(int ownerId)
        {
            return await _context.Hotels
                .Where(h => h.HotelOwnerId == ownerId)
                .Include(h => h.Rooms)
                .ToListAsync();
        }

        public async Task<Hotel?> GetHotelByIdAsync(int hotelId)
        {
            return await _context.Hotels
                .Include(h => h.Rooms)
                .FirstOrDefaultAsync(h => h.Id == hotelId);
        }

        public async Task UpdateHotelAsync(Hotel hotel)
        {
            _context.Hotels.Update(hotel);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteHotelAsync(int hotelId)
        {
            var hotel = await GetHotelByIdAsync(hotelId);
            if (hotel != null)
                _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Hotel>> GetHotelsWithRoomsAsync(string city, string country, decimal? maxPrice)
        {
            var query = _context.Hotels
                .Include(h => h.Rooms)
                .Where(h => h.City == city && h.Country == country && h.Rooms.Any(r => r.IsAvailable))
                .AsQueryable();

            if (maxPrice.HasValue)
            {
                query = query.Where(h => h.Rooms.Any(r => r.PricePerNight <= maxPrice.Value));
            }

            return await query.ToListAsync();
        }


        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
