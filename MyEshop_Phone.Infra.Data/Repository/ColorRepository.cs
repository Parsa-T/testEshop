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
    public class ColorRepository : IColorRepository
    {
        MyDbContext _db;
        public ColorRepository(MyDbContext context)
        {
            _db = context;
        }

        public async Task<_Color> AddAsync(_Color color)
        {
            _db.colors.Add(color);
            await _db.SaveChangesAsync();
            return color;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var color = await _db.colors.FindAsync(id);
            if(color==null)
                return false;
            _db.colors.Remove(color);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<_Color>> GetAllAsync()
        {
            return await _db.colors.ToListAsync();
        }
    }
}
