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
    public class ProductsFeaturseRepository : IProductsFeaturseRepository
    {
        MyDbContext _db;
        public ProductsFeaturseRepository(MyDbContext context)
        {
            _db = context;
        }

        public async Task AddAsync(_Products_Features products_Features)
        {
            _db.Products_Features.Add(products_Features);
            await Save();
        }

        public async Task<_Products_Features> AddProductsFeaturse(_Products_Features products_Features)
        {
            await _db.Products_Features.AddAsync(products_Features);
            return products_Features;
        }

        public async Task<bool> DeleteProductsFeatures(int id)
        {
            try
            {
                var pf = await _db.Products_Features.FindAsync(id);
                if (pf == null)
                    return false;
                _db.Products_Features.Remove(pf);
                await Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<_Products_Features>> GetAllFeaturse()
        {
            return await _db.Products_Features.Include(pf => pf.features).ToListAsync();
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<_Products_Features>> ShowAllProducts_Featurse()
        {
            return await _db.Products_Features.Include(pf => pf.products).Include(pf => pf.features).ToListAsync();
        }

        public async Task<IEnumerable<_Products_Features>> ShowfeatureById(int id)
        {
            return await _db.Products_Features.Where(pf => pf.ProductsId == id).Include(p=>p.features).ToListAsync();
        }
    }
}
