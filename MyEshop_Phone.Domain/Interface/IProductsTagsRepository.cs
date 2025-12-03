using MyEshop_Phone.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Domain.Interface
{
    public interface IProductsTagsRepository
    {
        Task<IEnumerable<_Products_Tags>> GetAllProductsTagsforId(int id);
        Task AddTags(_Products_Tags products_Tags);
        Task<bool> DeleteTags(int id);
        Task Save();
    }
}
