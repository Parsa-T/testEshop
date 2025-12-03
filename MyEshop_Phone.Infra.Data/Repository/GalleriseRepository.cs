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
    public class GalleriseRepository : IGalleriseRepository
    {
        MyDbContext _db;
        public GalleriseRepository(MyDbContext context)
        {
            _db = context;
        }

        public async Task AddAsync(_Products_Galleries entity)
        {
            await _db.Products_Galleries.AddAsync(entity);
        }

        public async Task DeleteAsync(_Products_Galleries entity)
        {
            _db.Products_Galleries.Remove(entity);
        }

        public async Task<List<_Products_Galleries>> GetAllAsync(int productId)
        {
            return await _db.Products_Galleries
            .Where(g => g.ProductsId == productId)
            .ToListAsync();
        }

        public async Task<_Products_Galleries?> GetByIdAsync(int id)
        {
            return await _db.Products_Galleries.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task<_Products_Galleries> ShowGalleris(int id)
        {
            return await _db.Products_Galleries.FirstOrDefaultAsync(g => g.Id == id);
        }
    }
}
