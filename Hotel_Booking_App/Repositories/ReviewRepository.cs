using Hotel_Booking_App.Contexts;
using Hotel_Booking_App.Exceptions;
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
            try
            {
                // Check if the customer exists
                var customer = await _context.Customers.FindAsync(customerId);
                if (customer == null)
                {
                    throw new EntityNotFoundException("Customer not found.");
                }

                // Check if the hotel exists
                var hotel = await _context.Hotels.FindAsync(reviewRequest.HotelId);
                if (hotel == null)
                {
                    throw new EntityNotFoundException("Hotel not found.");
                }

                // Create new review object
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

                return new ReviewResponseDto
                {
                    ReviewId = review.Id,
                    HotelId = review.HotelId,
                    //CustomerName = customer.Name, // Ensuring customer exists
                    Rating = review.Rating,
                    Comment = review.Comment,
                    CreatedAt = review.CreatedAt
                };
            }
            catch (EntityNotFoundException ex)
            {
                throw new EntityNotFoundException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the review.", ex);
            }
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
