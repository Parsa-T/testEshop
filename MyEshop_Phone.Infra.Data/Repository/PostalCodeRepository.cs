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
    public class PostalCodeRepository : ICodePostalRepository
    {
        MyDbContext _db;
        public PostalCodeRepository(MyDbContext context)
        {
            _db = context;
        }
        public async Task Add(_CodePostal codePostal)
        {
            await _db.CodePostals.AddAsync(codePostal);
        }

        public async Task DeletePostal(_CodePostal codePostal)
        {
            _db.CodePostals.Remove(codePostal);
        }

        public async Task<_CodePostal> FindPostalById(int id)
        {
            return await _db.CodePostals.Include(c => c.users).Include(p => p.products).FirstOrDefaultAsync(pc => pc.Id == id);
        }

        public async Task<IEnumerable<_CodePostal>> GetAllPostal()
        {
            return await _db.CodePostals.Include(c => c.users).Include(p => p.products).ToListAsync();
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }

        public async Task<List<_CodePostal>> ShowSenderForId(int id)
        {
            return await _db.CodePostals.Where(c=>c.UserId==id).Include(c=>c.products).Include(p=>p.users).ToListAsync();
        }
    }
}
