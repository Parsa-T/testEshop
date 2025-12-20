using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Application.Interface
{
    public interface IProductColorServices
    {
        Task<AddProductColorDTO> ShowAllColor();
        Task AddColorsToProduct(AddProductColorDTO dTO);
    }
    public interface IPColorServices
    {
        Task<List<ProductWithColorsDto>> GetProductsWithColors();
        Task<IEnumerable<_ProductsColor>> showAllColor();
        Task<bool> Delete(int id);
        Task<List<ProductWithColorsDto>> GetProductsWithColorsAsync(string? search);
    }
}
