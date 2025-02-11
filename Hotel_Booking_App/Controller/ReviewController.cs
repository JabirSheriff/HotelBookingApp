using Hotel_Booking_App.Interface.Review;
using Hotel_Booking_App.Models.DTOs.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hotel_Booking_App.Controllers
{
    [ApiController]
    [Route("api/reviews")]
    [Authorize]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] ReviewRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customerIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (customerIdClaim == null || !int.TryParse(customerIdClaim.Value, out int customerId))
            {
                return Unauthorized("Invalid token: Customer ID is missing.");
            }

            var review = await _reviewService.AddReviewAsync(request, customerId);
            return Ok(review);
        }


        [HttpGet("{hotelId}")]
        public async Task<IActionResult> GetReviewsByHotelId(int hotelId)
        {
            var reviews = await _reviewService.GetReviewsByHotelIdAsync(hotelId);
            return Ok(reviews);
        }
    }
}
