using Hotel_Booking_App.Interface.Hotel_Room;
using Hotel_Booking_App.Models;
using Hotel_Booking_App.Models.DTOs.Hotel_Room;
using AutoMapper;

namespace Hotel_Booking_App.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public RoomService(IRoomRepository roomRepository, IHotelRepository hotelRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        // ✅ Add Room
        public async Task<RoomResponseDto> AddRoomAsync(int ownerId, RoomRequestDto dto)
        {
            var hotel = await _hotelRepository.GetHotelByIdAsync(dto.HotelId);
            if (hotel == null || hotel.HotelOwnerId != ownerId)
            {
                throw new UnauthorizedAccessException("You can only add rooms to your own hotels.");
            }

            var room = _mapper.Map<Room>(dto);
            room.HotelId = hotel.Id;  // ✅ Explicitly set HotelId

            await _roomRepository.AddRoomAsync(room);
            await _roomRepository.SaveChangesAsync();

            return _mapper.Map<RoomResponseDto>(room);
        }


        // ✅ Get Rooms By Hotel ID
        public async Task<IEnumerable<RoomResponseDto>> GetRoomsByHotelIdAsync(int hotelId)
        {
            var rooms = await _roomRepository.GetRoomsByHotelIdAsync(hotelId);
            return _mapper.Map<IEnumerable<RoomResponseDto>>(rooms);
        }

        // ✅ Update Room
        public async Task<RoomResponseDto> UpdateRoomAsync(int ownerId, int roomId, RoomRequestDto dto)
        {
            var room = await _roomRepository.GetRoomByIdAsync(roomId);
            if (room == null)
            {
                throw new KeyNotFoundException("Room not found.");
            }

            var hotel = await _hotelRepository.GetHotelByIdAsync(room.HotelId);
            if (hotel == null || hotel.HotelOwnerId != ownerId)
            {
                throw new UnauthorizedAccessException("You can only update rooms in your own hotels.");
            }

            // Update room properties
            _mapper.Map(dto, room);

            await _roomRepository.UpdateRoomAsync(room);
            await _roomRepository.SaveChangesAsync();

            return _mapper.Map<RoomResponseDto>(room);
        }


        // ✅ Delete Room
        public async Task<bool> DeleteRoomAsync(int ownerId, int roomId)
        {
            var room = await _roomRepository.GetRoomByIdAsync(roomId);
            if (room == null)
            {
                return false; // Room not found
            }

            var hotel = await _hotelRepository.GetHotelByIdAsync(room.HotelId);
            if (hotel == null || hotel.HotelOwnerId != ownerId)
            {
                return false; // Unauthorized access
            }

            await _roomRepository.DeleteRoomAsync(room);
            await _roomRepository.SaveChangesAsync();
            return true; // Successfully deleted
        }

    }

}
