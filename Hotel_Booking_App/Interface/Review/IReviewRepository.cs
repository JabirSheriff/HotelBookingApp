using Hotel_Booking_App.Models.DTOs.Review;

namespace Hotel_Booking_App.Interface.Review
{
    public interface IReviewRepository
    {
        Task<ReviewResponseDto> AddReviewAsync(ReviewRequestDto reviewRequest, int customerId);
        Task<List<ReviewResponseDto>> GetReviewsByHotelIdAsync(int hotelId);
    }
}
