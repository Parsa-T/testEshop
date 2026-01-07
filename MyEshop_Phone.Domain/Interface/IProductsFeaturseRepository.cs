using MyEshop_Phone.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Domain.Interface
{
    public interface IProductsFeaturseRepository
    {
        Task<_Products_Features> AddProductsFeaturse(_Products_Features products_Features);
        Task<IEnumerable<_Products_Features>> GetAllFeaturse();
        Task AddAsync(_Products_Features products_Features);
        Task<IEnumerable<_Products_Features>> ShowAllProducts_Featurse();
        Task<bool> DeleteProductsFeatures(int id);
        Task<IEnumerable<_Products_Features>> ShowfeatureById(int id);
        Task Save();
    }
}
