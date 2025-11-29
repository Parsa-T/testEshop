using MyEshop_Phone.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Domain.Interface
{
    public interface IProductsGroupeRepository
    {
        Task<IEnumerable<_Products_Groups>> GetAllGroups();
        Task<_Products_Groups> AddGroupe(_Products_Groups products_Groups);
        Task<_Products_Groups> GetGroupsById(int id);
        Task DeleteGroups(_Products_Groups Products);
        Task Save();
    }
}
