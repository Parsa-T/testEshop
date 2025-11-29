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
    }
}
