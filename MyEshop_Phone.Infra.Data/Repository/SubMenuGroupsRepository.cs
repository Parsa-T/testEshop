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
    public class SubMenuGroupsRepository : ISubMenuGroupsRepository
    {
        MyDbContext _db;
        public SubMenuGroupsRepository(MyDbContext context)
        {
            _db = context;
        }

        public async Task AddSubMenu(_SubmenuGroups submenuGroups)
        {
            await _db.submenuGroups.AddAsync(submenuGroups);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteSubMenu(_SubmenuGroups submenuGroups)
        {
            _db.submenuGroups.Remove(submenuGroups);
            await _db.SaveChangesAsync();
        }

        public async Task<_SubmenuGroups> FindForIdSubMenu(int id)
        {
            return await _db.submenuGroups.FindAsync(id);
        }

        public async Task<IEnumerable<_SubmenuGroups>> GetAllSubGroups()
        {
            return await _db.submenuGroups.Include(sg => sg.groups).ToListAsync();
        }

        public async Task<_SubmenuGroups> ShowSubMenuForId(int id)
        {
            return await _db.submenuGroups.Where(sg => sg.Id == id).Include(g => g.groups).SingleOrDefaultAsync();
        }

        public async Task UpdateSubMenu(_SubmenuGroups submenuGroups)
        {
             _db.submenuGroups.Update(submenuGroups);
            await _db.SaveChangesAsync();
        }
    }
}
