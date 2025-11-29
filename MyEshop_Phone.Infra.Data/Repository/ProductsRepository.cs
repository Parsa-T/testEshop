using Microsoft.EntityFrameworkCore;
using MyEshop_Phone.Application.DTO;
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
    public class ProductsRepository : IProductsRepository
    {
        MyDbContext _db;
        public ProductsRepository(MyDbContext context)
        {
            _db = context;
        }
        public async Task<int> ProductsCount()
        {
            var result = await _db.Products.CountAsync();
            return result;
        }

        public async Task<IEnumerable<_Products>> GetAllProducts()
        {
            return await _db.Products.Include(p=>p.products_Groups).ToListAsync();
        }
    }
}
