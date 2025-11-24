using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Domain.Interface;
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
        public async Task<int> GetCountUser()
        {
            return await _userRepository.UserCount();
        }
    }
}
