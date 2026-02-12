using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Application.Interface
{
    public interface ISubGroupsServices
    {
        Task<SubMenuGroupsDTO> ShowAllSubGroups();
        Task<IEnumerable<_SubmenuGroups>> ShowSubMenuGroups();
        Task AddSubMenuAsync(_SubmenuGroups submenuGroups);
        Task<SubMenuGroupsDTO?> GetSubGroupsForId(int id);
        Task UpdateSubMenuAsync(SubMenuGroupsDTO dTO);
        Task<IEnumerable<_Products>> ShowSubMenuById(int id);
    }
}
