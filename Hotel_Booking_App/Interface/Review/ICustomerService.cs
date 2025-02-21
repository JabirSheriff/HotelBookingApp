using Hotel_Booking_App.Models;

namespace Hotel_Booking_App.Interface.Review
{
    public interface ICustomerService
    {
        Task<Customer> GetCustomerByUserIdAsync(int userId);
    }
}
