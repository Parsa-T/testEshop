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
            return await _db.Products.Include(p => p.products_Groups).ToListAsync();
        }

        public async Task<IEnumerable<_Products>> GetAll()
        {
            return await _db.Products.ToListAsync();
        }

        public async Task AddProducts(_Products products)
        {
            _db.Products.Add(products);
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }

        public async Task<_Products> GetProductsById(int id)
        {
            return await _db.Products.FindAsync(id);
        }

        public async Task UpdateProducts(_Products products)
        {
            _db.Products.Update(products);
            await Save();
        }

        public async Task<_Products?> GetProductWithFeatures(int id)
        {
            return await _db.Products.Include(p => p.products_Features).ThenInclude(pf => pf.features).SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task Delete(_Products products)
        {
            _db.Products.Remove(products);
            await Save();
        }

        public async Task<_Products> GetProductsIdinGroups(int id)
        {
           return await _db.Products.Include(p => p.products_Groups).SingleOrDefaultAsync(pr => pr.Id == id);
        }
    }
}
