using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Domain.Model;

namespace MyEshop_Phone.Pages.Admin.Products
{
    public class IndexModel : PageModel
    {
        IProductsServices _productsServices;
        public IndexModel(IProductsServices products)
        {
            _productsServices = products;
        }
        [BindProperty]
        public IEnumerable<GetProductsDTO> GetAllProducts { get; set; }
        public async Task OnGet()
        {
            GetAllProducts = await _productsServices.GetAll();
        }
    }
}
