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
        private readonly IWebHostEnvironment _env;
        public DeleteModel(IProductsRepository products, IWebHostEnvironment env)
        {
            _productsRepository = products;
            _env = env;
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

            string uploadsFolder = Path.Combine(
_env.WebRootPath,
"AdminPanel",
"Photo",
"Products"
);
            if (!string.IsNullOrEmpty(pro.ImageName))
            {
                string filePath = Path.Combine(uploadsFolder, pro.ImageName);

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
            //string filePath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "AdminPanel", "Photo", "Products", DeleteProducts.Id
            //        + ".png");
            //if (System.IO.File.Exists(filePath2))
            //    System.IO.File.Delete(filePath2);

            //string filePath3 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "AdminPanel", "Photo", "Products", DeleteProducts.Id
            //    + ".jpg");
            //if (System.IO.File.Exists(filePath3))
            //    System.IO.File.Delete(filePath3);
            await _productsRepository.Delete(pro);
            return RedirectToPage("index");
        }
    }
}
