using Hotel_Booking_App.Interface.Payment;
using Hotel_Booking_App.Models.DTOs.Payment;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotel_Booking_App.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<PaymentResponseDto> ProcessPaymentAsync(PaymentRequestDto paymentRequest)
        {
            return await _paymentRepository.ProcessPaymentAsync(paymentRequest);
        }

        public async Task<List<PaymentResponseDto>> GetPaidBookingsAsync()
        {
            return await _paymentRepository.GetPaidBookingsAsync();
        }

        public async Task<List<PaymentResponseDto>> GetUnpaidBookingsAsync()
        {
            return await _paymentRepository.GetUnpaidBookingsAsync();
        }
    }
}
