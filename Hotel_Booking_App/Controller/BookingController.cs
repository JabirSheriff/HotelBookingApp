using Hotel_Booking_App.Interface.Bookings;
using Hotel_Booking_App.Models.DTOs.Booking;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hotel_Booking_App.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        // ✅ Create Booking - Only Customers
        [Authorize(Roles = "Customer")]
        [HttpPost("add")]
        public async Task<IActionResult> CreateBooking([FromBody] BookingRequestDto requestDto)
        {
            if (requestDto == null)
                return BadRequest("Invalid booking request.");

            if (requestDto.HotelId <= 0 || requestDto.NumberOfRooms <= 0)
                return BadRequest("Hotel ID and Number of Rooms must be greater than zero.");

            if (requestDto.CheckInDate >= requestDto.CheckOutDate)
                return BadRequest("Check-out date must be after check-in date.");

            // 🔥 Fix: Ensure `CustomerId` is retrieved correctly
            var customerIdClaim = User.FindFirst("customerId")?.Value;
            if (!int.TryParse(customerIdClaim, out int customerId))
                return Unauthorized("Invalid customer ID.");

            try
            {
                var result = await _bookingService.CreateBookingAsync(customerId, requestDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        // ✅ Get Booking by ID (Only if Payment is Made)
        [Authorize(Roles = "Customer, Admin")]
        [HttpGet("{bookingId:int}")]
        public async Task<IActionResult> GetBookingById(int bookingId)
        {
            var booking = await _bookingService.GetBookingByIdAsync(bookingId);
            if (booking == null) return NotFound("Booking not found or payment not made.");
            return Ok(booking);
        }

        // ✅ Update Booking (Allowed Anytime)
        [Authorize(Roles = "Customer")]
        [HttpPut("{bookingId:int}")]
        public async Task<IActionResult> UpdateBooking(int bookingId, [FromBody] UpdateBookingRequestDto updateDto)
        {
            if (updateDto == null)
                return BadRequest("Invalid update request.");

            var success = await _bookingService.UpdateBookingAsync(bookingId, updateDto);
            if (!success) return NotFound("Booking not found.");
            return Ok("Booking updated successfully.");
        }

        // ✅ Delete Booking (Allowed Anytime)
        [Authorize(Roles = "Customer")]
        [HttpDelete("{bookingId:int}")]
        public async Task<IActionResult> DeleteBooking(int bookingId)
        {
            var success = await _bookingService.DeleteBookingAsync(bookingId);
            if (!success) return NotFound("Booking not found.");
            return Ok("Booking deleted successfully.");
        }
    }
}
