using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Application.Interface;

namespace MyEshop_Phone.Pages.Admin.Products_Tags
{
    public class AddModel : PageModel
    {
        ITagsServices _tagsServices;
        IProductsServices _productsServices;
        public AddModel(ITagsServices tagsServices, IProductsServices productsServices)
        {
            _tagsServices = tagsServices;
            _productsServices = productsServices;
        }
        [BindProperty]
        public AddTagsForProductsDTO AddTags { get; set; }
        public async Task OnGet()
        {
            AddTags = new AddTagsForProductsDTO
            {
                products = await _productsServices.GetProductsDropDown()
            };
        }
        public async Task<IActionResult> OnPost()
        {
            ModelState.Remove("AddTags.products");
            if (!ModelState.IsValid)
            {
                AddTags.products = await _productsServices.GetProductsDropDown();
                return Page();
            }
            await _tagsServices.AddAsync(AddTags);
            return RedirectToPage("Ditelse", new { id = AddTags.ProductsId });
        }
    }
}
