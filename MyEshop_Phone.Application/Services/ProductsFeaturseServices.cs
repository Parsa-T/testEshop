using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Domain.Interface;
using MyEshop_Phone.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MyEshop_Phone.Application.DTO.GetFeaturseAndProductsFeaturseDTO;

namespace MyEshop_Phone.Application.Services
{
    public class ProductsFeaturseServices : IProductsFeaturseServices
    {
        IProductsRepository _productsRepository;
        IProductsFeaturseRepository _productsFeaturseRepository;
        IFeaturseRepository _featurseRepository;
        public ProductsFeaturseServices(IProductsRepository products,IProductsFeaturseRepository repository,IFeaturseRepository featurse)
        {
            _productsRepository = products;
            _productsFeaturseRepository = repository;
            _featurseRepository = featurse;
        }

        public async Task AddFeaturse(AddFeaturesAndProductsFeaturesDTO dTO)
        {
            var productsFeatures = new _Products_Features
            {
                FeaturesId = dTO.FeaturesId,
                ProductsId = dTO.ProductsId,
                Value = dTO.Value,
            };
            await _productsFeaturseRepository.AddAsync(productsFeatures);
        }

        public async Task AddProductsFeaturseAsync(_Products_Features products_Features)
        {
            await _productsFeaturseRepository.AddProductsFeaturse(products_Features);
        }

        public async Task<IEnumerable<AddProductsFeaturseDTO>> GetAllAsync()
        {
            var featurse = await _productsFeaturseRepository.GetAllFeaturse();
            return featurse.Select(feat => new AddProductsFeaturseDTO
            {
                FeaturesId = feat.FeaturesId,
                Id = feat.Id,
                ProductsId = feat.ProductsId,
                Value = feat.Value,
            }).ToList();
        }

        public async Task<GetFeaturseAndProductsFeaturseDTO.ProductDetailsDTO?> GetProductDetails(int id)
        {
            var product = await _productsRepository.GetProductWithFeatures(id);
            return new ProductDetailsDTO
            {
                Id = product.Id,
                FeaturesTitle = product.Title,
                Features = product.products_Features.Select(f=>new ProductsFeaturseDTO
                {
                    Id = f.Id,
                    FeatureId = f.Id,
                    FeatureTitle = f.features.FeaturesTitle,
                    Value  = f.Value,
                }).ToList()
            };
        }

        public async Task Save()
        {
            await _productsFeaturseRepository.Save();
        }

        public async Task<IEnumerable<_Products_Features>> ShowProductsFeaturse()
        {
            return await _productsFeaturseRepository.ShowAllProducts_Featurse();
        }
    }
}
