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
    public class ProductsTagsRepository : IProductsTagsRepository
    {
        MyDbContext _db;
        public ProductsTagsRepository(MyDbContext context)
        {
            _db = context;
        }

        public async Task AddTags(_Products_Tags products_Tags)
        {
            await _db.Products_Tags.AddAsync(products_Tags);
            await Save();
        }

        public async Task<bool> DeleteTags(int id)
        {
            try
            {
                var pt = await _db.Products_Tags.FindAsync(id);
                if(pt==null)
                    return false;
                _db.Products_Tags.Remove(pt);
                await Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<_Products_Tags>> GetAllProductsTagsforId(int id)
        {
           return await _db.Products_Tags.Include(pt=>pt.products).Where(p=>p.ProductsId == id).ToListAsync();
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
