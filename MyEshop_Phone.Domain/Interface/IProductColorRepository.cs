using MyEshop_Phone.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Domain.Interface
{
    public interface IProductColorRepository
    {
        Task<IEnumerable<_ProductsColor>> GetAllAsync();
        Task AddProductColor(_ProductsColor productsColor);
        Task<IEnumerable<_ProductsColor>> ShowColorByID(int id);
        Task Save();
    }
}
