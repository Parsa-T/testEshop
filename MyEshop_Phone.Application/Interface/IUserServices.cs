using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Application.Interface
{
    public interface IUserServices
    {
        Task<int> GetCountUser();
        Task<IEnumerable<_Users>> GetUsers();
        Task<int> AddUsers(_Users users);
        Task SaveAsync();
        Task<AddOrEditUsersDTO> GetUserforEdit(int id);
        Task UserDelete(_Users users);
        Task<bool> FindNumberAsync(string number);
    }
}
