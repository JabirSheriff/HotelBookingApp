using Hotel_Booking_App.Contexts;
using Hotel_Booking_App.Interface.Payment;
using Hotel_Booking_App.Models;
using Hotel_Booking_App.Models.DTOs.Payment;
using HotelBookingApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Booking_App.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly HotelBookingDbContext _context;

        public PaymentRepository(HotelBookingDbContext context)
        {
            _context = context;
        }

        public async Task<PaymentResponseDto> ProcessPaymentAsync(PaymentRequestDto paymentRequest)
        {
            var booking = await _context.Bookings
                .Include(b => b.Customer)
                .FirstOrDefaultAsync(b => b.Id == paymentRequest.BookingId); // 🔹 Fixed BookingId issue

            if (booking == null)
                throw new KeyNotFoundException("Booking not found.");

            // Get all rooms linked to this booking
            var bookedRooms = await _context.BookingRooms
                .Where(br => br.BookingId == booking.Id) // 🔹 Fixed BookingId
                .Include(br => br.Room)
                .ThenInclude(r => r.Hotel) // Load Hotel data
                .ToListAsync();

            if (!bookedRooms.Any())
                throw new InvalidOperationException("No rooms found for this booking.");

            // Calculate total amount
            decimal totalAmount = bookedRooms.Sum(br => br.Room.PricePerNight) * (booking.CheckOutDate - booking.CheckInDate).Days;

            var payment = new Payment
            {
                BookingId = booking.Id, // 🔹 Fixed BookingId reference
                CustomerId = booking.Customer.Id, // 🔹 Fixed CustomerId reference
                Amount = totalAmount,
                PaymentMethod = Enum.Parse<PaymentMethod>(paymentRequest.PaymentMethod, true),
                Status = PaymentStatus.Completed,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // Save payment
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            // Update booking status to "Paid"
            booking.Status = BookingStatus.Paid;
            await _context.SaveChangesAsync();

            return new PaymentResponseDto
            {
                PaymentId = payment.Id,
                BookingId = booking.Id,
                AmountPaid = payment.Amount,
                PaymentMethod = payment.PaymentMethod.ToString(),
                PaymentDate = payment.CreatedAt,
                PaymentStatus = payment.Status.ToString(),
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckOutDate,
                RoomPricePerNight = bookedRooms.First().Room.PricePerNight
            };
        }

        public async Task<List<PaymentResponseDto>> GetPaidBookingsAsync()
        {
            return await _context.Payments
                .Include(p => p.Booking)
                .ThenInclude(b => b.Customer)
                .Include(p => p.Booking)
                .ThenInclude(b => b.BookingRooms)
                .ThenInclude(br => br.Room)
                .ThenInclude(r => r.Hotel)
                .Where(p => p.Status == PaymentStatus.Completed)
                .Select(p => new PaymentResponseDto
                {
                    PaymentId = p.Id,
                    BookingId = p.Booking.Id, // 🔹 Fixed BookingId reference
                    AmountPaid = p.Amount,
                    PaymentMethod = p.PaymentMethod.ToString(),
                    PaymentDate = p.CreatedAt,
                    PaymentStatus = p.Status.ToString(),
                    CheckInDate = p.Booking.CheckInDate,
                    CheckOutDate = p.Booking.CheckOutDate,
                    RoomPricePerNight = p.Booking.BookingRooms.FirstOrDefault().Room.PricePerNight // 🔹 Used FirstOrDefault() to prevent errors
                })
                .ToListAsync();
        }

        public async Task<List<PaymentResponseDto>> GetUnpaidBookingsAsync()
        {
            var unpaidBookings = await _context.Bookings
                .Include(b => b.BookingRooms)
                .ThenInclude(br => br.Room)
                .ThenInclude(r => r.Hotel)
                .Where(b => !_context.Payments.Any(p => p.BookingId == b.Id && p.Status == PaymentStatus.Completed))
                .ToListAsync(); // 🔹 Fetch data first

            return unpaidBookings.Select(b => new PaymentResponseDto
            {
                BookingId = b.Id,
                AmountPaid = 0,
                PaymentMethod = "Not Paid",
                PaymentDate = DateTime.MinValue,
                PaymentStatus = "Unpaid",
                CheckInDate = b.CheckInDate,
                CheckOutDate = b.CheckOutDate,
                RoomPricePerNight = b.BookingRooms.FirstOrDefault() != null ? b.BookingRooms.First().Room.PricePerNight : 0 // 🔹 Null check applied after fetching
            }).ToList();
        }
    }
}
