using MyEshop_Phone.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Domain.Interface
{
    public interface ISubMenuGroupsRepository
    {
        Task<IEnumerable<_SubmenuGroups>> GetAllSubGroups();
        Task AddSubMenu(_SubmenuGroups submenuGroups);
        Task<_SubmenuGroups> FindForIdSubMenu(int id);
        Task UpdateSubMenu(_SubmenuGroups submenuGroups);
        Task DeleteSubMenu(_SubmenuGroups submenuGroups);
        Task<_SubmenuGroups> ShowSubMenuForId(int id);
    }
}
