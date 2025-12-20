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
    public class ProductColorServices : IProductColorServices
    {
        IProductsServices _services;
        IColorRepository _colorRepository;
        IProductColorRepository _productColorRepository;
        public ProductColorServices(IProductsServices services, IColorRepository colorRepository, IProductColorRepository productColor)
        {
            _services = services;
            _productColorRepository = productColor;
            _colorRepository = colorRepository;
        }

        public async Task AddColorsToProduct(AddProductColorDTO dTO)
        {
            foreach (var colorId in dTO.SelectedColorIds)
            {
                var pcolor = new _ProductsColor
                {
                    ProductId = dTO.ProductId,
                    ColorId = colorId,
                };
                await _productColorRepository.AddProductColor(pcolor);
            }
            await _productColorRepository.Save();
        }

        public async Task<AddProductColorDTO> ShowAllColor()
        {
            var result = await _services.GetAll();
            var listpro = result.Select(p => new ProductsShowDTO
            {
                Id = p.Id,
                Title = p.Title,
            });
            var result2 = await _colorRepository.GetAllAsync();
            var listcolor = result2.Select(c => new ShowColorDTO
            {
                Id = c.Id,
                Name = c.Name,
            });
            return new AddProductColorDTO
            {
                colors = listcolor,
                productsShows = listpro,
            };
        }
    }
}
