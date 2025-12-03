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
    public class FeaturseRepository : IFeaturseRepository
    {
        MyDbContext _db;
        public FeaturseRepository(MyDbContext context)
        {
            _db = context;
        }
        public async Task AddFeaturse(_Features features)
        {
            _db.Features.Add(features);
            await Save();
        }

        public async Task<IEnumerable<_Features>> GetAll()
        {
            return await _db.Features.ToListAsync();
        }

        public async Task<_Features?> GetByTitleAsync(string title)
        {
            return await _db.Features.SingleOrDefaultAsync(f => f.FeaturesTitle == title);
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
