using Hotel_Booking_App.Interface.Bookings;
using Hotel_Booking_App.Models.DTOs.Booking;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [Authorize(Roles = "Customer")]
        [HttpPost("Add Booking")]
        public async Task<IActionResult> CreateBooking([FromBody] BookingRequestDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = int.Parse(User.FindFirstValue("nameId"));

            var bookingResponse = await _bookingService.CreateBookingAsync(userId, requestDto);

            return Ok(bookingResponse);
        }

        //// ✅ Get All Bookings by Customer (Only if Payment is Made)
        //[HttpGet("customer/{Bookings}")]
        //public async Task<IActionResult> GetAllBookingsByCustomer(int customerId)
        //{
        //    var bookings = await _bookingService.GetAllBookingsByCustomerAsync(customerId);
        //    return Ok(bookings);
        //}

        // ✅ Get Booking by ID (Only if Payment is Made)
        [HttpGet("{Booking By Id }")]
        public async Task<IActionResult> GetBookingById(int bookingId)
        {
            var booking = await _bookingService.GetBookingByIdAsync(bookingId);
            if (booking == null) return NotFound("Booking not found or payment not made");
            return Ok(booking);
        }

        // ✅ Update Booking (Allowed Anytime)
        [HttpPut("{Update Booking}")]
        public async Task<IActionResult> UpdateBooking(int bookingId, [FromBody] UpdateBookingRequestDto updateDto)
        {
            var success = await _bookingService.UpdateBookingAsync(bookingId, updateDto);
            if (!success) return NotFound("Booking not found");
            return Ok("Booking updated successfully");
        }

        // ✅ Delete Booking (Allowed Anytime)
        [HttpDelete("{Delete Booking}")]
        public async Task<IActionResult> DeleteBooking(int bookingId)
        {
            var success = await _bookingService.DeleteBookingAsync(bookingId);
            if (!success) return NotFound("Booking not found");
            return Ok("Booking deleted successfully");
        }
    }
}
