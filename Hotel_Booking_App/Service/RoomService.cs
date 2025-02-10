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

        public async Task<RoomResponseDto> AddRoomAsync(AddRoomDto dto)
        {
            var hotel = await _hotelRepository.GetHotelByIdAsync(dto.HotelId);
            if (hotel == null) throw new Exception("Hotel not found.");

            var room = _mapper.Map<Room>(dto);
            await _roomRepository.AddRoomAsync(room);
            await _roomRepository.SaveChangesAsync();

            return _mapper.Map<RoomResponseDto>(room);
        }

        public async Task<bool> UpdateRoomAsync(int roomId, AddRoomDto dto)
        {
            var room = await _roomRepository.GetRoomByIdAsync(roomId);
            if (room == null) return false;

            _mapper.Map(dto, room);
            await _roomRepository.UpdateRoomAsync(room);
            return await _roomRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteRoomAsync(int roomId)
        {
            await _roomRepository.DeleteRoomAsync(roomId);
            return await _roomRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<RoomResponseDto>> GetRoomsByHotelIdAsync(int hotelId)
        {
            var rooms = await _roomRepository.GetRoomsByHotelIdAsync(hotelId);
            return _mapper.Map<IEnumerable<RoomResponseDto>>(rooms);
        }

        public async Task<RoomResponseDto?> GetRoomByIdAsync(int roomId)
        {
            var room = await _roomRepository.GetRoomByIdAsync(roomId);
            return room != null ? _mapper.Map<RoomResponseDto>(room) : null;
        }
    }
}
