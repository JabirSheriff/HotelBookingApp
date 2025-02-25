using AutoMapper;
using Hotel_Booking_App.Interface.Bookings;
using Hotel_Booking_App.Models;
using Hotel_Booking_App.Models.DTOs.Booking;
using System;
using System.Linq;
using System.Threading.Tasks;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IMapper _mapper;

    public BookingService(IBookingRepository bookingRepository, IMapper mapper)
    {
        _bookingRepository = bookingRepository;
        _mapper = mapper;
    }

    public async Task<BookingResponseDto> CreateBookingAsync(int customerId, BookingRequestDto requestDto)
    {
        var roomType = (RoomType)requestDto.RoomType;

        var availableRooms = await _bookingRepository.GetAvailableRoomsAsync(
            requestDto.HotelId,
            requestDto.NumberOfRooms,
            roomType,
            requestDto.NumberOfGuests,
            requestDto.CheckInDate,
            requestDto.CheckOutDate
        );

        if (availableRooms.Count < requestDto.NumberOfRooms)
            throw new Exception($"Only {availableRooms.Count} room(s) available for the selected dates.");

        var booking = _mapper.Map<Booking>(requestDto);
        booking.CustomerId = customerId;
        booking.Status = BookingStatus.Pending;

        // ✅ Fix the night calculation
        var checkInDate = requestDto.CheckInDate.Date;
        var checkOutDate = requestDto.CheckOutDate.Date;
        var numberOfNights = (checkOutDate - checkInDate).Days;

        if (numberOfNights <= 0)
            throw new Exception("Check-out date must be after check-in date.");

        booking.TotalPrice = availableRooms.Sum(r => r.PricePerNight) * numberOfNights;

        foreach (var room in availableRooms)
        {
            booking.BookingRooms.Add(new BookingRoom { RoomId = room.Id });
        }

        await _bookingRepository.AddBookingAsync(booking);
        return _mapper.Map<BookingResponseDto>(booking);
    }


    public async Task<BookingResponseDto?> GetBookingByIdAsync(int bookingId)
    {
        var booking = await _bookingRepository.GetBookingByIdAsync(bookingId);

        if (booking == null)
            return null;

        var bookingDto = _mapper.Map<BookingResponseDto>(booking);

        // ✅ Ensure HotelName is included
        bookingDto.HotelName = booking.Hotel?.Name ?? "Unknown Hotel";

        return bookingDto;
    }

    public async Task<IEnumerable<BookingResponseDto>> GetAllBookingsByCustomerAsync(int customerId)
    {
        var bookings = await _bookingRepository.GetAllBookingsByCustomerAsync(customerId);
        return bookings.Select(b => _mapper.Map<BookingResponseDto>(b));
    }

    public async Task<bool> UpdateBookingAsync(int bookingId, UpdateBookingRequestDto updateDto)
    {
        var booking = await _bookingRepository.GetBookingByIdAsync(bookingId);
        if (booking == null) return false;

        booking.SpecialRequest = updateDto.SpecialRequest;
        return await _bookingRepository.UpdateBookingAsync(booking);
    }

    public async Task<bool> DeleteBookingAsync(int bookingId)
    {
        return await _bookingRepository.DeleteBookingAsync(bookingId);
    }
}
