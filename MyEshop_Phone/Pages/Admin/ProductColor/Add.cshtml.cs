using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Application.Interface;

namespace MyEshop_Phone.Pages.Admin.ProductColor
{
    public class AddModel : PageModel
    {
        IProductColorServices _productColor;
        public AddModel(IProductColorServices product)
        {
            _productColor = product;
        }
        [BindProperty]
        public AddProductColorDTO ProducColor { get; set; }
        public async Task OnGet()
        {
            ProducColor = await _productColor.ShowAllColor();
        }
        public async Task<IActionResult> OnPost()
        {
            ModelState.Remove("ProducColor.ColorId");
            ModelState.Remove("ProducColor.productsShows");
            ModelState.Remove("ProducColor.colors");
            if (!ModelState.IsValid)
            {
                await OnGet();
                return Page();
            }
            await _productColor.AddColorsToProduct(ProducColor);
            return RedirectToPage("Delete");
        }
    }
}
