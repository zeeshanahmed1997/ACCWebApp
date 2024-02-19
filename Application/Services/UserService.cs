using System;
using Domain.Interfaces;
using Domain.Models;
using Domain.Repositories;

namespace Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task AddUserAsync(User users)
        {
            await _userRepository.AddAsync(users);
        }

        public async Task UpdateUserAsync(User users)
        {
            await _userRepository.UpdateAsync(users);
        }

        public async Task DeleteUserAsync(User users)
        {
            await _userRepository.DeleteAsync(users);
        }
    }
}


