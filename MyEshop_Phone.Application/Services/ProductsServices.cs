using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Application.Services
{
    public class ProductsServices : IProductsServices
    {
        IProductsRepository _productsRepository;
        public ProductsServices(IProductsRepository repository)
        {
           _productsRepository = repository;
        }
        public async Task<int> GetProductsCount()
        {
            return await _productsRepository.ProductsCount();
        }
    }
}
