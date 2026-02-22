using MyEshop_Phone.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Application.Interface
{
    public interface IOrderServices
    {
        Task<_Orders> FinIdOrder(int id);
        Task SaveAsync();
        Task<_Orders> FindOrderByAuthorityAsync(string authority);
        Task AddOrderAsunc(_Orders orders);
        Task<int> CountOrderAsync();
    }
}
