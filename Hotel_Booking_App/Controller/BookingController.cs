using Hotel_Booking_App.Models;
using Hotel_Booking_App.Models.DTOs.Booking;
using HotelBookingApp.Interfaces;
using HotelBookingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HotelBookingApp.Controllers
{
    [Route("api/booking")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> CreateBooking([FromBody] BookingRequestDto request)
        {
            try
            {
                var customerId = int.Parse(User.FindFirst("customerId")?.Value ?? throw new UnauthorizedAccessException("Invalid customer ID"));

                // Convert int RoomType to RoomType enum (frontend sends 0-3, backend expects 1-4)
                var roomType = (RoomType)(request.RoomType + 1);

                var booking = await _bookingService.CreateBookingAsync(
                    customerId,
                    request.HotelId,
                    roomType,           // Pass converted RoomType
                    request.CheckInDate,
                    request.CheckOutDate,
                    request.NumberOfRooms,
                    request.NumberOfGuests,
                    request.SpecialRequest
                );
                return Ok(booking);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{bookingId}")]
        public async Task<IActionResult> GetBooking(int bookingId)
        {
            var booking = await _bookingService.GetBookingByIdAsync(bookingId);
            if (booking == null) return NotFound();
            return Ok(booking);
        }

        [HttpGet("customer")]
        public async Task<IActionResult> GetCustomerBookings()
        {
            var customerId = int.Parse(User.FindFirst("customerId")?.Value ?? throw new UnauthorizedAccessException("Invalid customer ID"));
            var bookings = await _bookingService.GetBookingsByCustomerIdAsync(customerId);
            return Ok(bookings);
        }

        [HttpPut("{bookingId}")]
        public async Task<IActionResult> UpdateBooking(int bookingId, DateTime checkInDate, DateTime checkOutDate, int numberOfGuests, string? specialRequest)
        {
            try
            {
                var success = await _bookingService.UpdateBookingAsync(bookingId, checkInDate, checkOutDate, numberOfGuests, specialRequest);
                if (!success) return NotFound();
                return Ok("Booking updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{bookingId}")]
        public async Task<IActionResult> DeleteBooking(int bookingId)
        {
            var success = await _bookingService.DeleteBookingAsync(bookingId);
            if (!success) return NotFound();
            return Ok("Booking deleted");
        }
    }
}