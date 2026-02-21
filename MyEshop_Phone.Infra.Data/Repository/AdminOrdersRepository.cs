using Microsoft.EntityFrameworkCore;
using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Infra.Data.Repository
{
    public class AdminOrdersRepository : IAdminOrdersServices
    {
        MyDbContext _db;
        public AdminOrdersRepository(MyDbContext context)
        {
            _db = context;
        }
        public async Task<List<AdminOrderListDTO>> GetFinalOrders()
        {
            return await _db.Orders
       .Where(o => o.IsFinaly)
       .Select(o => new AdminOrderListDTO
       {
           OrderId = o.Id,
           FullName = o.users.Name + " " + o.users.Family,
           Date = o.Date,
           TotalPrice = (int)o.TotalPrice
       }).ToListAsync();
        }

        public async Task<AdminOrderPdfDto> GetOrderForPdf(int orderId)
        {
            return await _db.Orders
        .Where(o => o.Id == orderId && o.IsFinaly)
        .Select(o => new AdminOrderPdfDto
        {
            FullName = o.users.Name + " " + o.users.Family,
            Address = o.users.Address,
            PostalCode =o.users.PostalCode.ToString(),
            TotalPrice = (int)o.TotalPrice,
            CityName = o.users.CityName,
            StateName = o.users.StateName,
            Items = o.orderDetails.Select(d => new AdminOrderPdfItemDto
            {
                ProductTitle = d.ProductTitle,
                ColorName = d.ColorName,
                Count = d.Count,
                Price = (int)d.Price
            }).ToList()
        }).FirstOrDefaultAsync();
        }
    }
}
