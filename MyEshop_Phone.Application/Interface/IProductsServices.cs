using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Application.ViewModel;
using MyEshop_Phone.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Application.Interface
{
    public interface IProductsServices
    {
        Task<int> GetProductsCount();
        Task<IEnumerable<GetProductsDTO>> GetAll();
        Task<ShowProductsDTO> GetAllProducts();
        Task<int> RegisterProducts(_Products dTO);
        Task<ShowProductsDTO?> GetProductsForEdit(int id);
        Task EditProdutcs(ShowProductsDTO dTO);
        Task<IEnumerable<ProductsDropdownDTO>> GetProductsDropDown();
        Task<IEnumerable<_Products>> ShowRecommendedProducts();
        Task<IEnumerable<_Products>> BestSeller();
        Task<IEnumerable<_Products>> ShowProtectionPhone();
        Task<IEnumerable<_Products>> ReSearch(string search);
        Task<IEnumerable<_Products>> GetAllProduct();
        Task<IEnumerable<_Products>> ShowGroupsById(int id);
        Task<_Products> ShopCartItem(int id);
        Task Save();
    }
    public interface IQueriProductsServices
    {
        Task<_Products> ShowSingleProducts(int id);
        Task<List<ProductFullListDTO>> GetProductsFullData();
    }
}
