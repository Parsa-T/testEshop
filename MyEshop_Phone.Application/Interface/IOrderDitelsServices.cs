using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Application.Interface
{
    public interface IOrderDitelsServices
    {
        Task AddDitelsAsync(_OrderDetails orderDetails);
        Task SaveAsync();
    }
    public interface IOrderQuery
    {
        Task<List<UserOrderDto>> GetUserOrders(int userId);
    }
}
