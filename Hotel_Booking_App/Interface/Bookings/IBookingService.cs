using Hotel_Booking_App.Models.DTOs.Booking;

namespace Hotel_Booking_App.Interface.Bookings
{
    public interface IBookingService
    {
        Task<BookingResponseDto> CreateBookingAsync(int userId, BookingRequestDto requestDto);
        Task<List<BookingResponseDto>> GetAllBookingsByCustomerAsync(int customerId);
        Task<BookingResponseDto?> GetBookingByIdAsync(int bookingId);
        Task<bool> UpdateBookingAsync(int bookingId, UpdateBookingRequestDto updateDto);
        Task<bool> DeleteBookingAsync(int bookingId);
    }
}
