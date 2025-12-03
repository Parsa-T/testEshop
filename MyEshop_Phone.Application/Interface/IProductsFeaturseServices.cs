using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MyEshop_Phone.Application.DTO.GetFeaturseAndProductsFeaturseDTO;

namespace MyEshop_Phone.Application.Interface
{
    public interface IProductsFeaturseServices
    {
        Task<ProductDetailsDTO?> GetProductDetails(int id);
        Task AddProductsFeaturseAsync(_Products_Features products_Features);
        Task<IEnumerable<AddProductsFeaturseDTO>> GetAllAsync();
        Task AddFeaturse(AddFeaturesAndProductsFeaturesDTO dTO);
        Task<IEnumerable<_Products_Features>> ShowProductsFeaturse();
        Task Save();
    }
}
