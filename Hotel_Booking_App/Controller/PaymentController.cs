using Hotel_Booking_App.Interface.Payment;
using Hotel_Booking_App.Models.DTOs.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotel_Booking_App.Controllers
{
    [Route("api/payments")]
    [ApiController]
    [Authorize] // Ensuring only authenticated users can access
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("process")]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequestDto paymentRequest)
        {
            var paymentResponse = await _paymentService.ProcessPaymentAsync(paymentRequest);
            return Ok(paymentResponse);
        }

        [HttpGet("paid-bookings")]
        public async Task<ActionResult<List<PaymentResponseDto>>> GetPaidBookings()
        {
            var paidBookings = await _paymentService.GetPaidBookingsAsync();
            return Ok(paidBookings);
        }

        [HttpGet("unpaid-bookings")]
        public async Task<ActionResult<List<PaymentResponseDto>>> GetUnpaidBookings()
        {
            var unpaidBookings = await _paymentService.GetUnpaidBookingsAsync();
            return Ok(unpaidBookings);
        }
    }
}
