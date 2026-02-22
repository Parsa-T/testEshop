using Microsoft.EntityFrameworkCore;
using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Domain.Interface;
using MyEshop_Phone.Domain.Model;
using MyEshop_Phone.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Infra.Data.Repository
{
    public class OrderDitelsRepository : IOrderDitelsRepository
    {
        MyDbContext _db;
        public OrderDitelsRepository(MyDbContext context)
        {
            _db = context;
        }
        public async Task AddOrderDitels(_OrderDetails orderDetails)
        {
            await _db.OrderDetails.AddAsync(orderDetails);
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
    public class OrderQuery : IOrderQuery
    {
        MyDbContext _db;
        public OrderQuery(MyDbContext context)
        {
            _db= context;
        }
        public async Task<List<UserOrderDto>> GetUserOrders(int userId) 
        {
            return await _db.Orders.Where(o => o.UserId == userId && o.IsFinaly).Select(o => new UserOrderDto
            {
                OrderId = o.Id,
                Date = o.Date,
                TotalPrice = (int)o.TotalPrice,
                Items = o.orderDetails.Select(d => new UserOrderDetailDto
                {
                    ProductTitle = d.ProductTitle,
                    ColorName = d.ColorName,
                    Count = d.Count,
                    Price = (int)d.Price,
                    ImageUrl = d.products.ImageName,
                }).ToList()
            }).ToListAsync();
        }
    }
}
