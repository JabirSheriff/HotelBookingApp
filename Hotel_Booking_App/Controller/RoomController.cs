using Hotel_Booking_App.Interface.Hotel_Room;
using Hotel_Booking_App.Models.DTOs.Hotel_Room;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Booking_App.Controllers
{
    [Route("api/rooms")]
    [ApiController]
    [Authorize(Roles = "HotelOwner")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddRoom([FromBody] AddRoomDto dto)
        {
            var room = await _roomService.AddRoomAsync(dto);
            return Ok(room);
        }

        [HttpGet("{hotelId}")]
        public async Task<IActionResult> GetRoomsByHotel(int hotelId)
        {
            var rooms = await _roomService.GetRoomsByHotelIdAsync(hotelId);
            return Ok(rooms);
        }

        [HttpGet("details/{roomId}")]
        public async Task<IActionResult> GetRoomById(int roomId)
        {
            var room = await _roomService.GetRoomByIdAsync(roomId);
            if (room == null) return NotFound("Room not found.");
            return Ok(room);
        }

        [HttpPut("{roomId}")]
        public async Task<IActionResult> UpdateRoom(int roomId, [FromBody] AddRoomDto dto)
        {
            var success = await _roomService.UpdateRoomAsync(roomId, dto);
            if (!success) return NotFound("Room not found.");
            return Ok("Room updated successfully.");
        }

        [HttpDelete("{roomId}")]
        public async Task<IActionResult> DeleteRoom(int roomId)
        {
            var success = await _roomService.DeleteRoomAsync(roomId);
            if (!success) return NotFound("Room not found.");
            return Ok("Room deleted successfully.");
        }
    }
}
