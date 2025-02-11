using Hotel_Booking_App.Models.DTOs.Review;

namespace Hotel_Booking_App.Interface.Review
{
    public interface IReviewService
    {
        Task<ReviewResponseDto> AddReviewAsync(ReviewRequestDto request, int customerId);
        Task<List<ReviewResponseDto>> GetReviewsByHotelIdAsync(int hotelId);
    }
}
