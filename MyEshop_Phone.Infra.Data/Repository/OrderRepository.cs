using Microsoft.EntityFrameworkCore;
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
    public class OrderRepository : IOrderRepository
    {
        MyDbContext _db;
        public OrderRepository(MyDbContext context)
        {
            _db = context;
        }

        public async Task AddOrder(_Orders orders)
        {
            await _db.Orders.AddAsync(orders);
        }

        public async Task<int> CountOrders()
        {
            return await _db.Orders.Where(o => o.IsFinaly == true).CountAsync();
        }

        public async Task DeleteOrder(_Orders orders)
        {
            _db.Orders.Remove(orders);
            await _db.SaveChangesAsync();
        }

        public async Task<_Orders> FindByAuthority(string authority)
        {
            return await _db.Orders.FirstOrDefaultAsync(o => o.Authority == authority);
        }

        public async Task<_Orders> FindById(int id)
        {
            return await _db.Orders.FindAsync(id);
        }

        public async Task IsFainally()
        {
             await _db.Orders.Where(o=>o.IsFinaly).ToListAsync();
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
