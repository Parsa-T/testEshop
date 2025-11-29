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
    public class ProductsGroupsRepository : IProductsGroupeRepository
    {
        MyDbContext _db;
        public ProductsGroupsRepository(MyDbContext context)
        {
            _db = context;
        }

        public async Task<_Products_Groups> AddGroupe(_Products_Groups products_Groups)
        {
            await _db.Products_Groups.AddAsync(products_Groups);
            return products_Groups;
        }

        public async Task DeleteGroups(_Products_Groups Products)
        {
             _db.Products_Groups.Remove(Products);
        }

        public async Task<IEnumerable<_Products_Groups>> GetAllGroups()
        {
            return await _db.Products_Groups.ToListAsync();
        }

        public async Task<_Products_Groups> GetGroupsById(int id)
        {
            return await _db.Products_Groups.FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
