using Microsoft.AspNetCore.Http.HttpResults;
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
    public class OrderServices : IOrderServices
    {
        IOrderRepository _orderRepository;
        public OrderServices(IOrderRepository order)
        {
            _orderRepository = order;
        }

        public async Task AddOrderAsunc(_Orders orders)
        {
            await _orderRepository.AddOrder(orders);
        }

        public async Task<int> CountOrderAsync()
        {
            return await _orderRepository.CountOrders();
        }

        public async Task<_Orders> FindOrderByAuthorityAsync(string authority)
        {
            if (authority == null)
                return null;
            return await _orderRepository.FindByAuthority(authority);
        }

        public async Task<_Orders> FinIdOrder(int id)
        {
            if (id == null)
                return null;
            return await _orderRepository.FindById(id);
        }

        public async Task SaveAsync()
        {
           await _orderRepository.Save();
        }
    }
}
