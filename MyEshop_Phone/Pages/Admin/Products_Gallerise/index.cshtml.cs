using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Domain.Interface;
using MyEshop_Phone.Domain.Model;

namespace MyEshop_Phone.Pages.Admin.Products_Gallerise
{
    public class indexModel : PageModel
    {
        IProductsGalleriseServices _productsGalleriseServices;
        public indexModel(IProductsGalleriseServices services)
        {
            _productsGalleriseServices = services;
        }
        public List<AddGalleriseDTO> Galleries { get; set; }
        [BindProperty]
        public AddGalleriseDTO Input { get; set; }
        [BindProperty(SupportsGet = true)]
        public int ProductId { get; set; }
        public async Task OnGetAsync(int productId)
        {
            Galleries = await _productsGalleriseServices.GetAllAsync(ProductId);
        }
        public async Task<IActionResult> OnPostAddAsync()
        {
            if (!ModelState.IsValid)
            {
                Galleries = await _productsGalleriseServices.GetAllAsync(ProductId);
                return Page();
            }

            Input.ProductsId = ProductId;
            await _productsGalleriseServices.AddAsync(Input);
            return RedirectToPage(new { ProductId = ProductId });
        }
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> OnPostDeleteAsync([FromForm] int id)
        {
            if (id <= 0)
                return Page(); // یا می‌توان alert ساده داد

            await _productsGalleriseServices.DeleteAsync(id);

            // بعد از حذف دوباره صفحه را بارگذاری می‌کنیم یا می‌توانیم Partial Render کنیم
            return RedirectToPage(new { ProductId = ProductId });
        }

    }
}
