using Microsoft.AspNetCore.Http.HttpResults;
using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Domain.Interface;
using MyEshop_Phone.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Application.Services
{
    public class UserServices : IUserServices
    {
        IUserRepository _userRepository;
        public UserServices(IUserRepository user)
        {
            _userRepository = user;
        }

        public async Task<int> AddUsers(_Users users)
        {
            await _userRepository.AddUser(users);
            return users.Id;
        }

        public async Task<bool> FindNumberAsync(string number)
        {
            return await _userRepository.FindByNumber(number);
        }

        public async Task<EditNumberUsersDTO> FindUserIdForEditNumber(int id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
                return null;
            return new EditNumberUsersDTO
            {
                Id = user.Id,
                Number = user.Number,
            };
        }

        public async Task<int> GetCountUser()
        {
            return await _userRepository.UserCount();
        }

        public async Task<AddOrEditUsersDTO> GetUserforEdit(int id)
        {
            var user = await _userRepository.GetUserById(id);

            if (user == null)
                return null;
            return new AddOrEditUsersDTO
            {
                Address = user.Address,
                Family = user.Family,
                Id = user.Id,
                IsAdmin = user.IsAdmin,
                Number = user.Number,
                Name = user.Name,
                RegisterDate = user.RegisterDate,
                PostalCode = user.PostalCode,
            };
        }

        public async Task<IEnumerable<_Users>> GetUsers()
        {
            return await _userRepository.GetAllUsers();
        }

        public async Task SaveAsync()
        {
            await _userRepository.Save();
        }

        public async Task UpdateUserNumber(int id, string number)
        {
            var user =await _userRepository.GetUserById(id);
            user.Number = number;
            await _userRepository.Save();
        }

        public async Task UserDelete(_Users users)
        {
            _userRepository.DeleteUsers(users);
        }

        public async Task<EditUserProfileDTO> UserforEdit(int id)
        {
            var user = await _userRepository.GetUserById(id);

            if (user == null)
                return null;
            return new EditUserProfileDTO
            {
                Address = user.Address,
                Family = user.Family,
                Id = user.Id,
                Name = user.Name,
                PostalCode = user.PostalCode,
                UrlPhoto = user.UrlPhoto,
                CityName = user.CityName,
                StateName = user.StateName,
            };
        }
    }
}
