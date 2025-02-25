﻿using Hotel_Booking_App.Models.DTOs.Hotel_Room;

namespace Hotel_Booking_App.Interface.Hotel_Room
{
    public interface IHotelService
    {
        Task<IEnumerable<HotelResponseDto>> GetAllHotelsAsync();
        Task<HotelResponseDto> AddHotelAsync(int ownerId, AddHotelDto dto);
        Task<IEnumerable<HotelResponseDto>> GetHotelsByOwnerIdAsync(int ownerId);
        Task<Hotel?> GetHotelEntityByIdAsync(int hotelId);
        Task<HotelResponseDto?> GetHotelByIdAsync(int hotelId);
        Task<bool> UpdateHotelAsync(int hotelId, AddHotelDto dto);
        Task<bool> DeleteHotelAsync(int hotelId);
        Task<List<HotelResponseDto>> SearchHotelsWithRoomsAsync(RoomSearchRequestDto request);
    }
}
