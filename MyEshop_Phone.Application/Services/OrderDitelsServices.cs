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
    public class OrderDitelsServices : IOrderDitelsServices
    {
        IOrderDitelsRepository _orderDitelsRepository;
        public OrderDitelsServices(IOrderDitelsRepository orderDitels)
        {
            _orderDitelsRepository = orderDitels;
        }
        public async Task AddDitelsAsync(_OrderDetails orderDetails)
        {
            await _orderDitelsRepository.AddOrderDitels(orderDetails);
        }

        public async Task SaveAsync()
        {
            await _orderDitelsRepository.Save();
        }
    }
}
