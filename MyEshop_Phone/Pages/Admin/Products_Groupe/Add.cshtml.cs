using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Domain.Model;

namespace MyEshop_Phone.Pages.Admin.Products_Groupe
{
    public class AddModel : PageModel
    {
        IProductsGroupServices _productsGroupServices;
        public AddModel(IProductsGroupServices products)
        {
            _productsGroupServices = products;
        }
        [BindProperty]
        public AddOrEditGroupsDTO AddGroups { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost() 
        {
            _Products_Groups products_Groups = new _Products_Groups()
            {
                GroupTitle = AddGroups.GroupTitle,
            };
            if (!ModelState.IsValid)
                return Page();
            await _productsGroupServices.AddGroups(products_Groups);
            await _productsGroupServices.Save();
            return RedirectToPage("index");
        }
    }
}
