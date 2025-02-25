using Hotel_Booking_App.Models;

namespace Hotel_Booking_App.Interface.Bookings
{
    public interface IBookingRepository
    {
        Task<bool> AddBookingAsync(Booking booking);
        Task<List<Room>> GetAvailableRoomsAsync(int hotelId, int numberOfRooms, RoomType roomType, int numberOfGuests, DateTime checkIn, DateTime checkOut);
        Task<Booking?> GetBookingByIdAsync(int bookingId);
        Task<IEnumerable<Booking>> GetAllBookingsByCustomerAsync(int customerId);
        Task<bool> UpdateBookingAsync(Booking booking);
        Task<bool> DeleteBookingAsync(int bookingId);
        Task<bool> SaveChangesAsync();
    }
}
