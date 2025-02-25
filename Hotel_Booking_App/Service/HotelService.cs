using Hotel_Booking_App.Interface.Hotel_Room;
using Hotel_Booking_App.Models;
using Hotel_Booking_App.Models.DTOs.Hotel_Room;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;


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


        // ...

        public async Task<IEnumerable<HotelResponseDto>> GetAllHotelsAsync()
        {
            var hotels = await _hotelRepository.GetAllHotelsAsync();
            return _mapper.Map<IEnumerable<HotelResponseDto>>(hotels);
        }



        public async Task<HotelResponseDto> AddHotelAsync(int ownerId, AddHotelDto dto)
        {
            var hotel = _mapper.Map<Hotel>(dto);
            hotel.HotelOwnerId = ownerId;
            await _hotelRepository.AddHotelAsync(hotel);
            return _mapper.Map<HotelResponseDto>(hotel);
        }





        public async Task<IEnumerable<HotelResponseDto>> GetHotelsByOwnerIdAsync(int ownerId)
        {
            var hotels = await _hotelRepository.GetHotelsByOwnerIdAsync(ownerId);
            return _mapper.Map<IEnumerable<HotelResponseDto>>(hotels);
        }

        public async Task<HotelResponseDto?> GetHotelByIdAsync(int hotelId)
        {
            var hotel = await _hotelRepository.GetHotelByIdAsync(hotelId);
            return hotel != null ? _mapper.Map<HotelResponseDto>(hotel) : null;
        }

        public async Task<Hotel?> GetHotelEntityByIdAsync(int hotelId)
        {
            return await _hotelRepository.GetHotelByIdAsync(hotelId); // Fetch full entity
        }




        public async Task<bool> UpdateHotelAsync(int hotelId, AddHotelDto dto)
        {
            var hotel = await _hotelRepository.GetHotelByIdAsync(hotelId);
            if (hotel == null) return false;

            // Manually update fields (so only changed values are updated)
            hotel.Name = dto.Name ?? hotel.Name;
            hotel.Address = dto.Address ?? hotel.Address;
            hotel.City = dto.City ?? hotel.City;
            hotel.Country = dto.Country ?? hotel.Country;
            hotel.StarRating = dto.StarRating ?? hotel.StarRating;
            hotel.Description = dto.Description ?? hotel.Description;
            hotel.ContactEmail = dto.ContactEmail ?? hotel.ContactEmail;
            hotel.ContactPhone = dto.ContactPhone ?? hotel.ContactPhone;
            hotel.IsActive = dto.IsActive ?? hotel.IsActive;

            await _hotelRepository.UpdateHotelAsync(hotel);
            return true;
        }


        public async Task<bool> DeleteHotelAsync(int hotelId)
        {
            var hotel = await _hotelRepository.GetHotelByIdAsync(hotelId);
            if (hotel == null) return false;

            await _hotelRepository.DeleteHotelAsync(hotelId);
            return true;
        }

        public async Task<List<HotelResponseDto>> SearchHotelsWithRoomsAsync(RoomSearchRequestDto request)
        {
            var hotels = await _hotelRepository.GetHotelsWithRoomsAsync(request.City, request.Country, request.MaxPrice);
            return _mapper.Map<List<HotelResponseDto>>(hotels);
        }
    }

}