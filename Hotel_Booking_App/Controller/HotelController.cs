using Hotel_Booking_App.Interface.Hotel_Room;
using Hotel_Booking_App.Models.DTOs.Hotel_Room;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hotel_Booking_App.Controllers
{
    [Route("api/hotels")]
    [ApiController]
    [Authorize(Roles = "HotelOwner")]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddHotel([FromBody] AddHotelDto dto)
        {
            var ownerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var hotel = await _hotelService.AddHotelAsync(ownerId, dto);
            return Ok(hotel);
        }

        [HttpGet]
        public async Task<IActionResult> GetHotelsByOwner()
        {
            var ownerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var hotels = await _hotelService.GetHotelsByOwnerIdAsync(ownerId);
            return Ok(hotels);
        }

        [HttpGet("{hotelId}")]
        public async Task<IActionResult> GetHotelById(int hotelId)
        {
            var hotel = await _hotelService.GetHotelByIdAsync(hotelId);
            if (hotel == null) return NotFound("Hotel not found.");
            return Ok(hotel);
        }

        [HttpPut("{hotelId}")]
        public async Task<IActionResult> UpdateHotel(int hotelId, [FromBody] AddHotelDto dto)
        {
            var success = await _hotelService.UpdateHotelAsync(hotelId, dto);
            if (!success) return NotFound("Hotel not found.");
            return Ok("Hotel updated successfully.");
        }

        [HttpDelete("{hotelId}")]
        public async Task<IActionResult> DeleteHotel(int hotelId)
        {
            var success = await _hotelService.DeleteHotelAsync(hotelId);
            if (!success) return NotFound("Hotel not found.");
            return Ok("Hotel deleted successfully.");
        }
    }
}
