using MyEshop_Phone.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Domain.Interface
{
    public interface IOrderRepository
    {
        Task<_Orders> FindById(int id);
        Task DeleteOrder(_Orders orders);
        Task Save();
        Task<_Orders> FindByAuthority(string authority);
        Task AddOrder(_Orders orders);
        Task<int> CountOrders();
        Task IsFainally();
    }
}
