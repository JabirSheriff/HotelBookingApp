﻿using Azure;
using Hotel_Booking_App.Interface.Hotel_Room;
using Hotel_Booking_App.Models.DTOs.Hotel_Room;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.JsonPatch;

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

        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllHotels()
        {
            var hotels = await _hotelService.GetAllHotelsAsync();
            return Ok(hotels);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddHotel([FromBody] AddHotelDto dto)
        {
            var ownerIdClaim = User.FindFirst("HotelOwnerId")?.Value;
            if (ownerIdClaim == null)
                return Unauthorized("Invalid token or missing HotelOwnerId.");

            int ownerId = int.Parse(ownerIdClaim);

            var hotel = await _hotelService.AddHotelAsync(ownerId, dto);
            return CreatedAtAction(nameof(GetHotelById), new { hotelId = hotel.Id }, hotel);
        }

        [HttpGet("by-owner")]
        public async Task<IActionResult> GetHotelsByOwner()
        {
            var ownerIdClaim = User.FindFirst("HotelOwnerId")?.Value;
            if (ownerIdClaim == null)
                return Unauthorized("Invalid token or missing HotelOwnerId.");

            int ownerId = int.Parse(ownerIdClaim);
            var hotels = await _hotelService.GetHotelsByOwnerIdAsync(ownerId);
            return Ok(hotels);
        }

        [HttpGet("{hotelId}")]
        public async Task<IActionResult> GetHotelById(int hotelId)
        {
            var hotel = await _hotelService.GetHotelByIdAsync(hotelId);
            if (hotel == null)
                return NotFound("Hotel not found.");
            return Ok(hotel);
        }

        [HttpPatch("{hotelId}")]
        public async Task<IActionResult> UpdateHotel(int hotelId, [FromBody] JsonPatchDocument<AddHotelDto> patchDto)
        {
            var hotel = await _hotelService.GetHotelEntityByIdAsync(hotelId);
            if (hotel == null) return NotFound("Hotel not found.");

            var hotelDto = new AddHotelDto
            {
                Name = hotel.Name,
                Address = hotel.Address,
                City = hotel.City,
                Country = hotel.Country,
                StarRating = hotel.StarRating,
                ContactEmail = hotel.ContactEmail,
                ContactPhone = hotel.ContactPhone,
                IsActive = hotel.IsActive
            };

            patchDto.ApplyTo(hotelDto, ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _hotelService.UpdateHotelAsync(hotelId, hotelDto);
            return success ? Ok("Hotel updated successfully.") : NotFound("Hotel not found.");
        }




        [HttpDelete("{hotelId}")]
        public async Task<IActionResult> DeleteHotel(int hotelId)
        {
            var ownerIdClaim = User.FindFirst("HotelOwnerId")?.Value;
            if (ownerIdClaim == null)
                return Unauthorized("Invalid token or missing HotelOwnerId.");

            int ownerId = int.Parse(ownerIdClaim);

            var hotel = await _hotelService.GetHotelByIdAsync(hotelId);
            if (hotel == null || hotel.HotelOwnerId != ownerId)
                return Forbid("Unauthorized to delete this hotel.");

            var success = await _hotelService.DeleteHotelAsync(hotelId);
            if (!success) return NotFound("Hotel not found.");
            return Ok("Hotel deleted successfully.");
        }
    }
}
