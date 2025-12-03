using MyEshop_Phone.Application.DTO;
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
        Task Save();
    }
}
