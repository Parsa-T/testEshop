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
    public class TagsServices : ITagsServices
    {
        IProductsTagsRepository _productsTagsRepository;
        public TagsServices(IProductsTagsRepository productsTagsRepository)
        {
            _productsTagsRepository = productsTagsRepository;
        }

        public async Task AddAsync(AddTagsForProductsDTO dTO)
        {
            var tags = new _Products_Tags
            {
                Id = dTO.Id,
                ProductsId = dTO.ProductsId,
                Tag = dTO.Tag,
            };
            await _productsTagsRepository.AddTags(tags);
        }
    }
}
