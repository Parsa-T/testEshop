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

        public async Task<IEnumerable<_Products>> RecommendedProducts()
        {
            return await _db.Products.Where(p => p.RecommendedProducts == true).ToListAsync();
        }

        public async Task<IEnumerable<_Products>> BestSeller(int take = 7)
        {
            return _db.Products.OrderByDescending(p => p.CreateTime).Take(take);
        }

        public async Task<IEnumerable<_Products>> ProtectionPhone(int take = 7)
        {
            return await _db.Products.Where(p => EF.Functions.Like(p.Title, "%قاب%")).OrderByDescending(p => p.CreateTime).Take(take).ToListAsync();
        }

        public async Task<IEnumerable<_Products>> SearchProduct(string search)
        {
            return _db.Products.Where(p => p.Title.Contains(search) || p.ShortDescription.Contains(search) || p.Text.Contains(search)).Distinct();
        }

        public async Task<IEnumerable<_Products>> ShowAllProducts()
        {
            return await _db.Products.Include(pg => pg.products_Groups).Include(sub => sub.submenuGroups).ToListAsync();
        }

        public async Task<IEnumerable<_Products>> ShowProductsByGroupsId(int id)
        {
            return await _db.Products.Where(p => p.ProductGroupsId == id).Include(pg => pg.products_Groups).ToListAsync();
        }
    }
    public class QueriProductsRepository : IQueriProductsServices
    {
        MyDbContext _db;
        public QueriProductsRepository(MyDbContext context)
        {
            _db = context;
        }
        public async Task<_Products> ShowSingleProducts(int id)
        {
            var product = await _db.Products
                 .Include(pg => pg.products_Galleries)
                 .Include(pf => pf.products_Features)
                 .SingleOrDefaultAsync(p => p.Id == id);
            return product;
        }
    }
}
