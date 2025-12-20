using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Application.Utilitise;
using MyEshop_Phone.Domain.Interface;
using MyEshop_Phone.Domain.Model;

namespace MyEshop_Phone.Pages.Admin.Products
{
    public class DeleteModel : PageModel
    {
        IProductsRepository _productsRepository;
        public DeleteModel(IProductsRepository products)
        {
            _productsRepository = products;
        }
        [BindProperty]
        public _Products DeleteProducts { get; set; }
        public async Task OnGet(int id)
        {
            DeleteProducts = await _productsRepository.GetProductsIdinGroups(id);
        }
        public async Task<IActionResult> OnPost()
        {
            var pro = await _productsRepository.GetProductsById(DeleteProducts.Id);
            if(pro==null)
                return NotFound();
            string filePath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "AdminPanel", "Photo", "Products", DeleteProducts.Id
                    + ".png");
            if (System.IO.File.Exists(filePath2))
                System.IO.File.Delete(filePath2);

            string filePath3 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "AdminPanel", "Photo", "Products", DeleteProducts.Id
                + ".jpg");
            if (System.IO.File.Exists(filePath3))
                System.IO.File.Delete(filePath3);
             await _productsRepository.Delete(pro);
            return RedirectToPage("index");
        }
    }
}
