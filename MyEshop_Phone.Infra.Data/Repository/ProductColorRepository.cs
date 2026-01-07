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
    public class ProductColorRepository : IProductColorRepository
    {
        MyDbContext _db;
        public ProductColorRepository(MyDbContext context)
        {
            _db = context;
        }

        public async Task AddProductColor(_ProductsColor productsColor)
        {
            await _db.productsColors.AddAsync(productsColor);
        }

        public async Task<IEnumerable<_ProductsColor>> GetAllAsync()
        {
            return await _db.productsColors.ToListAsync();
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<_ProductsColor>> ShowColorByID(int id)
        {
            return await _db.productsColors.Where(pc => pc.ProductId == id).Include(p=>p.color).ToListAsync();
        }
    }
    public class PColorRepository : IPColorServices
    {
        MyDbContext _db;
        public PColorRepository(MyDbContext context)
        {
            _db = context;
        }

        public async Task<bool> Delete(int id)
        {
            var result = await _db.productsColors.FindAsync(id);
            if (result == null)
                return false;
            _db.productsColors.Remove(result);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<ProductWithColorsDto>> GetProductsWithColors()
        {
            return await _db.productsColors.Include(p => p.products).Include(c => c.color)
                .GroupBy(pc => new { pc.ProductId, pc.products.Title })
                .Select(g => new ProductWithColorsDto
                {
                    ProductId = g.Key.ProductId,
                    ProductTitle = g.Key.Title,
                    Colors = g.Select(c => new ShowColorDTO
                    {
                        Id = c.color.Id,
                        Name = c.color.Name,
                    }).ToList()
                }).ToListAsync();
            //throw new NotImplementedException();
        }

        public async Task<List<ProductWithColorsDto>> GetProductsWithColorsAsync(string? search)
        {
            var query = _db.productsColors.Include(p => p.products).Include(c => c.color).AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(pc => pc.products.Title.Contains(search));
            }
            return await query.GroupBy(pc => new { pc.ProductId, pc.products.Title })
                .Select(g => new ProductWithColorsDto
                {
                    ProductId = g.Key.ProductId,
                    ProductTitle = g.Key.Title,
                    Colors = g.Select(x => new ShowColorDTO
                    {
                        Id = x.color.Id,
                        Name = x.color.Name,
                    }).ToList()
                }).ToListAsync();
        }

        public async Task<IEnumerable<_ProductsColor>> showAllColor()
        {
            return await _db.productsColors.Include(p => p.products).Include(c => c.color).ToListAsync();
        }
    }
}
