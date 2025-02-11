using Hotel_Booking_App.Interface.Review;
using Hotel_Booking_App.Models.DTOs.Review;

namespace Hotel_Booking_App.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<ReviewResponseDto> AddReviewAsync(ReviewRequestDto request, int customerId)
        {
            return await _reviewRepository.AddReviewAsync(request, customerId);
        }

        public async Task<List<ReviewResponseDto>> GetReviewsByHotelIdAsync(int hotelId)
        {
            return await _reviewRepository.GetReviewsByHotelIdAsync(hotelId);
        }
    }
}
