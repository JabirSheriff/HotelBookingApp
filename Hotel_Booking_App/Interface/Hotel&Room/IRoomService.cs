using Hotel_Booking_App.Models.DTOs.Hotel_Room;

namespace Hotel_Booking_App.Interface.Hotel_Room
{
    public interface IRoomService
    {
        Task<RoomResponseDto> AddRoomAsync(AddRoomDto dto);
        Task<IEnumerable<RoomResponseDto>> GetRoomsByHotelIdAsync(int hotelId);
        Task<RoomResponseDto?> GetRoomByIdAsync(int roomId);
        Task<bool> UpdateRoomAsync(int roomId, AddRoomDto dto);
        Task<bool> DeleteRoomAsync(int roomId);
    }
}
