using Hotel_Booking_App.Interface.Hotel_Room;
using Hotel_Booking_App.Models.DTOs.Hotel_Room;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Booking_App.Controllers
{
    [Authorize(Roles = "HotelOwner")]
    [Route("api/rooms")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RoomController(IRoomService roomService, IHttpContextAccessor httpContextAccessor)
        {
            _roomService = roomService;
            _httpContextAccessor = httpContextAccessor;
        }

        // ✅ Add Room
        [HttpPost]
        [Authorize(Roles = "HotelOwner")] // ✅ Ensure only hotel owners can add rooms
        public async Task<IActionResult> AddRoom([FromBody] RoomRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var hotelOwnerIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("hotelOwnerId")?.Value;
            if (string.IsNullOrEmpty(hotelOwnerIdClaim))
                return Unauthorized("Hotel Owner ID claim not found in token.");

            var hotelOwnerId = int.Parse(hotelOwnerIdClaim);
            var room = await _roomService.AddRoomAsync(hotelOwnerId, dto);
            return CreatedAtAction(nameof(GetRoomsByHotelId), new { hotelId = room.HotelId }, room);
        }


        // ✅ Get Rooms by Hotel ID
        [HttpGet("{hotelId}")]
        public async Task<IActionResult> GetRoomsByHotelId(int hotelId)
        {
            var rooms = await _roomService.GetRoomsByHotelIdAsync(hotelId);
            return Ok(rooms);
        }

        [Authorize(Roles = "HotelOwner")]
        [HttpPut("{roomId}")]
        public async Task<IActionResult> UpdateRoom(int roomId, [FromBody] RoomRequestDto dto)
        {
            var hotelOwnerIdClaim = User.FindFirst("hotelOwnerId")?.Value;
            if (string.IsNullOrEmpty(hotelOwnerIdClaim) || !int.TryParse(hotelOwnerIdClaim, out int hotelOwnerId))
            {
                return Unauthorized("Invalid token or HotelOwnerId missing.");
            }

            var updatedRoom = await _roomService.UpdateRoomAsync(hotelOwnerId, roomId, dto);
            return Ok(updatedRoom);
        }


        // ✅ Delete Room
        [HttpDelete("{roomId}")]
        public async Task<IActionResult> DeleteRoom(int roomId)
        {
            var hotelOwnerIdClaim = User.FindFirst("hotelOwnerId")?.Value;
            if (string.IsNullOrEmpty(hotelOwnerIdClaim) || !int.TryParse(hotelOwnerIdClaim, out int hotelOwnerId))
            {
                return Unauthorized("Invalid token or HotelOwnerId missing.");
            }

            bool isDeleted = await _roomService.DeleteRoomAsync(hotelOwnerId, roomId);
            if (!isDeleted)
            {
                return NotFound("Room not found or you are not authorized to delete it.");
            }

            return Ok("Room deleted successfully.");
        }

    }

}
