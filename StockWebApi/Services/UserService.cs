﻿using StockWebApi.Models;
using StockWebApi.Repositories;
using StockWebApi.ViewModels;

namespace StockWebApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> GetUserById(int userId)
        {
            User user = await _userRepository.GetById(userId);
            return user;
        }

        public async Task<string> Login(LoginViewModel loginViewModel)
        {
            return await _userRepository.Login(loginViewModel);
        }

        public async Task<User?> Register(RegisterViewModel registerViewModel)
        {
            var existingUserByUsername = await _userRepository.GetByUsername(registerViewModel.Username ?? "");
            if(existingUserByUsername != null)
            {
                throw new ArgumentException("Username is already exists");
            }

            var existingUserByEmail = await _userRepository.GetByEmail(registerViewModel.Email);
            if(existingUserByEmail != null)
            {
                throw new ArgumentException("Email is already exists");
            }
            return await _userRepository.Create(registerViewModel);
        }
    }
}
