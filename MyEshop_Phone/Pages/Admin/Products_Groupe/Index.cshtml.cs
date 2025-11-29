using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Domain.Model;

namespace MyEshop_Phone.Pages.Admin.Products_Groupe
{
    public class IndexModel : PageModel
    {
        IProductsGroupServices _productsGroupServices;
        public IndexModel(IProductsGroupServices services)
        {
            _productsGroupServices = services;
        }
        public IEnumerable<_Products_Groups> GetAllGroups { get; set; }
        public async Task OnGet()
        {
            GetAllGroups = await _productsGroupServices.GetAll();
        }
    }
}
