using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Domain.Interface;
using MyEshop_Phone.Domain.Model;
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

        public async Task<IEnumerable<GetProductsDTO>> GetAll()
        {
            var product = await _productsRepository.GetAllProducts();
            if (product == null)
                return null;
            return product.Select(p => new GetProductsDTO
            {
                CreateTime = p.CreateTime,
                GroupsTitle = p.products_Groups.GroupTitle,
                Price = p.Price,
                ShortDescription = p.ShortDescription,
                Text = p.Text,
                Title = p.Title,
                ImageName = p.ImageName,
                ProductGroupsId = p.ProductGroupsId
            }).ToList();
        }

        public async Task<int> GetProductsCount()
        {
            return await _productsRepository.ProductsCount();
        }
    }
}
