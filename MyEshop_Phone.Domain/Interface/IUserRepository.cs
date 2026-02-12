using MyEshop_Phone.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Domain.Interface
{
    public interface IUserRepository
    {
        Task<int> UserCount();
        Task<IEnumerable<_Users>> GetAllUsers();
        Task AddUser(_Users users);
        Task Save();
        Task<_Users> GetUserById(int id);
        Task UpdateUser(_Users users);
        Task DeleteUsers(_Users users);
        Task<bool> FindByNumber(string number);
        Task<_Users> IsExistUser(string number);
    }
}
