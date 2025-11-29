using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Application.Interface
{
    public interface IProductsGroupServices
    {
        Task<IEnumerable<_Products_Groups>> GetAll();
        Task AddGroups(_Products_Groups products_Groups);
        Task<AddOrEditGroupsDTO> GetGroupsForEdit(int id);
        Task RemoveGroups(_Products_Groups groups);
        Task Save();
    }
}
