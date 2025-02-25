using Hotel_Booking_App.Contexts;
using Hotel_Booking_App.Interface.Bookings;
using Hotel_Booking_App.Models;
using Microsoft.EntityFrameworkCore;

public class BookingRepository : IBookingRepository
{
    private readonly HotelBookingDbContext _context;

    public BookingRepository(HotelBookingDbContext context)
    {
        _context = context;
    }

    public async Task<bool> AddBookingAsync(Booking booking)
    {
        await _context.Bookings.AddAsync(booking);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<List<Room>> GetAvailableRoomsAsync(int hotelId, int numberOfRooms, RoomType roomType, int numberOfGuests, DateTime checkIn, DateTime checkOut)
    {
        return await _context.Rooms
            .Where(r => r.HotelId == hotelId &&
                        r.Type == roomType &&
                        !_context.BookingRooms.Any(br =>
                            br.RoomId == r.Id &&
                            br.Booking.CheckInDate < checkOut &&
                            br.Booking.CheckOutDate > checkIn))
            .Take(numberOfRooms)
            .ToListAsync();
    }

    public async Task<Booking?> GetBookingByIdAsync(int bookingId)
    {
        return await _context.Bookings
            .Include(b => b.BookingRooms)
        .ThenInclude(br => br.Room)  // Include rooms
        .ThenInclude(r => r.Hotel)   // Include hotel
        .FirstOrDefaultAsync(b => b.Id == bookingId);
    }

    public async Task<IEnumerable<Booking>> GetAllBookingsByCustomerAsync(int customerId)
    {
        return await _context.Bookings
            .Where(b => b.CustomerId == customerId)
            .Include(b => b.Hotel)
            .Include(b => b.BookingRooms)
                .ThenInclude(br => br.Room)
            .ToListAsync();
    }

    public async Task<bool> UpdateBookingAsync(Booking booking)
    {
        _context.Bookings.Update(booking);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteBookingAsync(int bookingId)
    {
        var booking = await _context.Bookings
            .Include(b => b.BookingRooms)
            .FirstOrDefaultAsync(b => b.Id == bookingId);

        if (booking == null) return false;

        _context.BookingRooms.RemoveRange(booking.BookingRooms);
        _context.Bookings.Remove(booking);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
