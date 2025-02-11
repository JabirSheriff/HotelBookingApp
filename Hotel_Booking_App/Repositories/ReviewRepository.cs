using Hotel_Booking_App.Contexts;
using Hotel_Booking_App.Interface.Review;
using Hotel_Booking_App.Models;
using Hotel_Booking_App.Models.DTOs.Review;
using HotelBookingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Booking_App.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly HotelBookingDbContext _context;

        public ReviewRepository(HotelBookingDbContext context)
        {
            _context = context;
        }

        public async Task<ReviewResponseDto> AddReviewAsync(ReviewRequestDto reviewRequest, int customerId)
        {
            var review = new Review
            {
                CustomerId = customerId,
                HotelId = reviewRequest.HotelId,
                Rating = reviewRequest.Rating,
                Comment = reviewRequest.Comment,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            var customer = await _context.Customers.FindAsync(customerId);

            return new ReviewResponseDto
            {
                ReviewId = review.Id,
                HotelId = review.HotelId,
                //CustomerName = customer?.Name ?? "Unknown",
                Rating = review.Rating,
                Comment = review.Comment,
                CreatedAt = review.CreatedAt
            };
        }

        public async Task<List<ReviewResponseDto>> GetReviewsByHotelIdAsync(int hotelId)
        {
            return await _context.Reviews
                .Where(r => r.HotelId == hotelId)
                .Include(r => r.Customer)
                .Select(r => new ReviewResponseDto
                {
                    ReviewId = r.Id,
                    HotelId = r.HotelId,
                    //CustomerName = r.Customer.Name,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    CreatedAt = r.CreatedAt
                })
                .ToListAsync();
        }
    }
}
