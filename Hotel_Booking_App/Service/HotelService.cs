using Hotel_Booking_App.Interface.Hotel_Room;
using Hotel_Booking_App.Models;
using Hotel_Booking_App.Models.DTOs.Hotel_Room;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Booking_App.Services
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public HotelService(IHotelRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        // ✅ Add a new hotel
        public async Task<HotelResponseDto> AddHotelAsync(int ownerId, AddHotelDto dto)
        {
            // Check if the hotel owner exists
            //var ownerExists = await _hotelOwnerRepository.ExistsAsync(dto.HotelOwnerId);
            //if (!ownerExists)
            //{
            //    throw new Exception("Hotel owner does not exist.");
            //}

            // Proceed with adding the hotel
            var hotel = _mapper.Map<Hotel>(dto);
            hotel.HotelOwnerId = dto.HotelOwnerId;

            await _hotelRepository.AddHotelAsync(hotel);

            return _mapper.Map<HotelResponseDto>(hotel);
        }
        


        // ✅ Get all hotels owned by the logged-in owner
        public async Task<IEnumerable<HotelResponseDto>> GetHotelsByOwnerIdAsync(int ownerId)
        {
            var hotels = await _hotelRepository.GetHotelsByOwnerIdAsync(ownerId);
            return _mapper.Map<IEnumerable<HotelResponseDto>>(hotels);
        }

        // ✅ Get a specific hotel by ID
        public async Task<HotelResponseDto?> GetHotelByIdAsync(int hotelId)
        {
            var hotel = await _hotelRepository.GetHotelByIdAsync(hotelId);
            return hotel != null ? _mapper.Map<HotelResponseDto>(hotel) : null;
        }

        // ✅ Update a hotel (only if the owner matches)
        public async Task<bool> UpdateHotelAsync(int hotelId, AddHotelDto dto)
        {
            var hotel = await _hotelRepository.GetHotelByIdAsync(hotelId);
            if (hotel == null) return false;

            _mapper.Map(dto, hotel);
            await _hotelRepository.UpdateHotelAsync(hotel);
            return await _hotelRepository.SaveChangesAsync();
        }

        // ✅ Delete a hotel (only if the owner matches)
        public async Task<bool> DeleteHotelAsync(int hotelId)
        {
            var hotel = await _hotelRepository.GetHotelByIdAsync(hotelId);
            if (hotel == null) return false;

            await _hotelRepository.DeleteHotelAsync(hotelId);
            return await _hotelRepository.SaveChangesAsync();
        }
    }
}
