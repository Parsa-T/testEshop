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
        Task<_Products?> GetProductWithFeatures(int id);
        Task Delete(_Products products);
        Task<_Products> GetProductsIdinGroups(int id);
        Task<IEnumerable<_Products>> RecommendedProducts();
        Task<IEnumerable<_Products>> BestSeller(int take = 7);
        Task<IEnumerable<_Products>> ProtectionPhone(int take = 7);
        Task<IEnumerable<_Products>> SearchProduct(string search);
        Task<IEnumerable<_Products>> ShowAllProducts();
        Task<IEnumerable<_Products>> ShowProductsByGroupsId(int id);
        Task Save();
    }
}
