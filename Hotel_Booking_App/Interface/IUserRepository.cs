﻿using Hotel_Booking_App.Models;

namespace Hotel_Booking_App.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByEmailAsync(string email);
        Task AddUserAsync(User user);
        Task AddCustomerAsync(Customer customer);

        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
        Task<bool> SaveChangesAsync();
    }
}
