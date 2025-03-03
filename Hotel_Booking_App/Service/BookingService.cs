using Hotel_Booking_App.Models;
using HotelBookingApp.Interfaces;
using HotelBookingApp.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingApp.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<Booking> CreateBookingAsync(int customerId, int hotelId, RoomType roomType, DateTime checkIn, DateTime checkOut, int numberOfRooms, int numberOfGuests, string? specialRequest)
        {
            if (checkIn >= checkOut)
                throw new ArgumentException("Check-out date must be after check-in date.");

            var availableRooms = await _bookingRepository.GetAvailableRoomsAsync(hotelId, numberOfRooms, roomType, numberOfGuests, checkIn, checkOut);
            if (availableRooms.Count() < numberOfRooms)
            {
                var bookedUntil = await _bookingRepository.GetBookingsByCustomerIdAsync(customerId)
                    .ContinueWith(t => t.Result
                        .Where(b => b.HotelId == hotelId &&
                                    b.BookingRooms.Any(br => br.Room.Type == roomType) &&
                                    b.CheckInDate < checkOut &&
                                    b.CheckOutDate > checkIn)
                        .OrderBy(b => b.CheckOutDate)
                        .FirstOrDefault()?.CheckOutDate);
                var message = bookedUntil.HasValue
                    ? $"Room booked until {bookedUntil.Value.ToString("yyyy-MM-dd")}. Only {availableRooms.Count()} available."
                    : $"Only {availableRooms.Count()} rooms available for selected dates.";
                throw new Exception(message);
            }

            var booking = new Booking
            {
                CustomerId = customerId,
                HotelId = hotelId,
                CheckInDate = checkIn,
                CheckOutDate = checkOut,
                NumberOfGuests = numberOfGuests,
                TotalPrice = availableRooms.Take(numberOfRooms).Sum(r => r.PricePerNight) * (checkOut - checkIn).Days,
                SpecialRequest = specialRequest
            };

            // Assign actual room IDs from availableRooms
            foreach (var room in availableRooms.Take(numberOfRooms))
            {
                booking.BookingRooms.Add(new BookingRoom { RoomId = room.Id });
            }

            if (booking.BookingRooms.Count == 0)
                throw new Exception("No valid rooms assigned to booking.");

            return await _bookingRepository.AddBookingAsync(booking);
        }

        public async Task<Booking?> GetBookingByIdAsync(int bookingId)
        {
            return await _bookingRepository.GetBookingByIdAsync(bookingId);
        }

        public async Task<List<Booking>> GetBookingsByCustomerIdAsync(int customerId)
        {
            return await _bookingRepository.GetBookingsByCustomerIdAsync(customerId);
        }

        public async Task<bool> UpdateBookingAsync(int bookingId, DateTime checkIn, DateTime checkOut, int numberOfGuests, string? specialRequest)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(bookingId);
            if (booking == null) return false;

            if (checkIn >= checkOut)
                throw new ArgumentException("Check-out date must be after check-in date.");

            var availableRooms = await _bookingRepository.GetAvailableRoomsAsync(
                booking.HotelId,
                booking.BookingRooms.Count(), // Use existing room count
                booking.BookingRooms.First().Room.Type,
                numberOfGuests,
                checkIn,
                checkOut
            );
            if (availableRooms.Count() < booking.BookingRooms.Count()) // Fixed Count -> Count()
                throw new Exception("Rooms not available for updated dates.");

            booking.CheckInDate = checkIn;
            booking.CheckOutDate = checkOut;
            booking.NumberOfGuests = numberOfGuests;
            booking.SpecialRequest = specialRequest;
            booking.TotalPrice = availableRooms.First().PricePerNight * (checkOut - checkIn).Days;

            return await _bookingRepository.UpdateBookingAsync(booking);
        }

        public async Task<bool> DeleteBookingAsync(int bookingId)
        {
            return await _bookingRepository.DeleteBookingAsync(bookingId);
        }

        public async Task<List<Booking>> GetBookingsByHotelIdsAsync(List<int> hotelIds)
        {
            var bookings = await _bookingRepository.GetBookingsByHotelIdsAsync(hotelIds);
            return bookings;
        }
    }
}