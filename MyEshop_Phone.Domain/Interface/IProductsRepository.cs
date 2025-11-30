using MyEshop_Phone.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Domain.Interface
{
    public interface IProductsRepository
    {
        Task<int> ProductsCount();
        Task<IEnumerable<_Products>> GetAllProducts();
        Task<IEnumerable<_Products>> GetAll();
        Task AddProducts(_Products products);
        Task<_Products> GetProductsById(int id);
        Task UpdateProducts(_Products products);
        Task Save();
    }
}
