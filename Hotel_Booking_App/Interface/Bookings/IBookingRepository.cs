using Hotel_Booking_App.Models;

namespace Hotel_Booking_App.Interface.Bookings
{
    public interface IBookingRepository
    {
        Task<List<Room>> GetAvailableRoomsAsync(int hotelId, RoomType roomType, int numberOfRooms);
        Task<Customer> GetCustomerByUserIdAsync(int userId);
        Task<Booking> CreateBookingAsync(Booking booking);
        Task<List<Booking>> GetAllBookingsByCustomerAsync(int customerId);
        Task<Booking?> GetBookingByIdAsync(int bookingId);
        Task<bool> UpdateBookingAsync(Booking booking);
        Task<bool> DeleteBookingAsync(int bookingId);
        Task<bool> SaveChangesAsync();
    }
}
