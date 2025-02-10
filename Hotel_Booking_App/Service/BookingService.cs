using AutoMapper;
using Hotel_Booking_App.Interface.Bookings;
using Hotel_Booking_App.Models.DTOs.Booking;
using Hotel_Booking_App.Models;
using Hotel_Booking_App.Exceptions;

namespace Hotel_Booking_App.Service
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public BookingService(IBookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        public async Task<BookingResponseDto> CreateBookingAsync(int userId, BookingRequestDto requestDto)
        {
            var customer = await _bookingRepository.GetCustomerByUserIdAsync(userId);
            if (customer == null)
                throw new UnauthorizedAccessException("Customer not found. Please log in.");

            var availableRooms = await _bookingRepository.GetAvailableRoomsAsync(
                requestDto.HotelId, requestDto.RoomType, requestDto.NumberOfRooms);

            if (availableRooms.Count < requestDto.NumberOfRooms)
                throw new Exception("Not enough rooms available.");

            var totalDays = (requestDto.CheckOutDate - requestDto.CheckInDate).Days;
            var totalPrice = availableRooms.Sum(r => r.PricePerNight) * totalDays;

            var booking = new Booking
            {
                CustomerId = customer.Id,
                HotelId = requestDto.HotelId,
                CheckInDate = requestDto.CheckInDate,
                CheckOutDate = requestDto.CheckOutDate,
                NumberOfGuests = requestDto.NumberOfGuests,
                TotalPrice = totalPrice,
                Status = BookingStatus.Pending,
                SpecialRequest = requestDto.SpecialRequest,
                BookingRooms = availableRooms.Select(room => new BookingRoom { RoomId = room.Id }).ToList()
            };

            var newBooking = await _bookingRepository.CreateBookingAsync(booking);

            return new BookingResponseDto
            {
                BookingId = newBooking.Id,
                HotelId = newBooking.HotelId,
                HotelName = newBooking.Hotel?.Name ?? "Unknown Hotel",
                CheckInDate = newBooking.CheckInDate,
                CheckOutDate = newBooking.CheckOutDate,
                NumberOfGuests = newBooking.NumberOfGuests,
                TotalPrice = newBooking.TotalPrice,
                Status = newBooking.Status,
                SpecialRequest = requestDto.SpecialRequest,
                //RoomsBooked = availableRooms.Select(r => new RoomDetailsDto
                //{
                //    RoomId = r.Id,
                //    RoomNumber = r.RoomNumber,
                //    RoomType = r.Type,
                //    PricePerNight = r.PricePerNight
                //}).ToList()
                // ✅ Return only the count of booked rooms
                RoomsBookedCount = booking.BookingRooms.Count
            };


        }

        // ✅ Get All Bookings by Customer
        public async Task<List<BookingResponseDto>> GetAllBookingsByCustomerAsync(int customerId)
        {
            var bookings = await _bookingRepository.GetAllBookingsByCustomerAsync(customerId);
            return _mapper.Map<List<BookingResponseDto>>(bookings);
        }

        // ✅ Get Booking by ID
        public async Task<BookingResponseDto?> GetBookingByIdAsync(int bookingId)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(bookingId);
            return booking == null ? null : _mapper.Map<BookingResponseDto>(booking);
        }

        // ✅ Update Booking
        public async Task<bool> UpdateBookingAsync(int bookingId, UpdateBookingRequestDto updateDto)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(bookingId);
            if (booking == null)
            {
                throw new Exception("Booking not found.");
            }

            // Update fields
            booking.CheckInDate = updateDto.CheckInDate;
            booking.CheckOutDate = updateDto.CheckOutDate;
            booking.NumberOfGuests = updateDto.NumberOfGuests;
            booking.SpecialRequest = updateDto.SpecialRequest;

            return await _bookingRepository.UpdateBookingAsync(booking);
        }

        // ✅ Delete Booking
        public async Task<bool> DeleteBookingAsync(int bookingId)
        {
            var deleted = await _bookingRepository.DeleteBookingAsync(bookingId);
            if (!deleted)
                throw new NotFoundException("Booking not found");

            return true;
        }
    }
}
