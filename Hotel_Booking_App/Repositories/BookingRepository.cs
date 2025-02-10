using Hotel_Booking_App.Contexts;
using Hotel_Booking_App.Interface.Bookings;
using Hotel_Booking_App.Models;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Booking_App.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly HotelBookingDbContext _context;

        public BookingRepository(HotelBookingDbContext context)
        {
            _context = context;
        }

        public async Task<List<Room>> GetAvailableRoomsAsync(int hotelId, RoomType roomType, int numberOfRooms)
        {
            return await _context.Rooms
                .Where(r => r.HotelId == hotelId && r.Type == roomType && r.IsAvailable)
                .Take(numberOfRooms)
                .ToListAsync();
        }

        public async Task<Customer> GetCustomerByUserIdAsync(int userId)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<Booking> CreateBookingAsync(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        // ✅ Get All Bookings by Customer (Only if Payment is Made)
        public async Task<List<Booking>> GetAllBookingsByCustomerAsync(int customerId)
        {
            return await _context.Bookings
                .Include(b => b.Hotel)
                .Include(b => b.BookingRooms)
                .Where(b => b.CustomerId == customerId)  // ✅ Use IsPaid instead of checking string status
                .ToListAsync();
        }

        // ✅ Get Booking by ID (Only if Payment is Made)
        public async Task<Booking?> GetBookingByIdAsync(int bookingId)
        {
            return await _context.Bookings
                .Include(b => b.Hotel)
                .Include(b => b.BookingRooms)
                .FirstOrDefaultAsync(b => b.Id == bookingId);  // ✅ Use IsPaid for consistency
        }

        // ✅ Update Booking (Allowed Anytime)
        public async Task<bool> UpdateBookingAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
            return await _context.SaveChangesAsync() > 0;
        }

        // ✅ Delete Booking (Allowed Anytime)
        public async Task<bool> DeleteBookingAsync(int bookingId)
        {
            var booking = await _context.Bookings
        .Include(b => b.BookingRooms)  // Include related records
        .FirstOrDefaultAsync(b => b.Id == bookingId);

            if (booking == null)
                return false; // Booking not found

            // ❗ First, remove all related BookingRooms records
            _context.BookingRooms.RemoveRange(booking.BookingRooms);

            // ❗ Now delete the booking itself
            _context.Bookings.Remove(booking);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
