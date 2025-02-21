//using Hotel_Booking_App.Exceptions;
//using Hotel_Booking_App.Interface.Review;
//using Hotel_Booking_App.Models.DTOs.Review;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System.Security.Claims;

//namespace Hotel_Booking_App.Controllers
//{
//    [ApiController]
//    [Route("api/reviews")]
//    [Authorize]
//    public class ReviewController : ControllerBase
//    {
//        private readonly IReviewService _reviewService;
//        private readonly ICustomerService _customerService;

//        public ReviewController(IReviewService reviewService)
//        {
//            _reviewService = reviewService;
            
//        }
//        [HttpPost("Add Reviews")]
//        public async Task<IActionResult> AddReview([FromBody] ReviewRequestDto request)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

//            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
//            {
//                return Unauthorized("Invalid token: User ID is missing.");
//            }

//            // Fetch CustomerId using UserId
//            var customer = await _customerService.GetCustomerByUserIdAsync(userId);
//            if (customer == null)
//            {
//                return Unauthorized("Customer account not found.");
//            }

//            var review = await _reviewService.AddReviewAsync(request, customer.Id);
//            return Ok(review);
//        }




//        [HttpGet("{Get Reviews}")]
//        public async Task<IActionResult> GetReviewsByHotelId(int hotelId)
//        {
//            var reviews = await _reviewService.GetReviewsByHotelIdAsync(hotelId);
//            return Ok(reviews);
//        }
//    }
//}
