using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Domain.Model;
using MyEshopPhone.Infra.Data.Migrations;

namespace MyEshop_Phone.Pages.Admin.Products
{
    public class EditModel : PageModel
    {
        IProductsServices _productsServices;
        private readonly IWebHostEnvironment _env;
        public EditModel(IProductsServices productsServices, IWebHostEnvironment env)
        {
            _productsServices = productsServices;
            _env = env;
        }
        [BindProperty]
        public ShowProductsDTO EditProducts { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        public async Task<IActionResult> OnGet()
        {
            EditProducts = await _productsServices.GetProductsForEdit(Id);
            if(EditProducts==null)
                return NotFound();
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            //if (!ModelState.IsValid)
            //    return Page();
            if (EditProducts.imgUp?.Length > 0 && EditProducts.imgUp != null)
            {
                string uploadsFolder = Path.Combine(
_env.WebRootPath,
"AdminPanel",
"Photo",
                "Products"
                );

                Directory.CreateDirectory(uploadsFolder);
                if (!string.IsNullOrEmpty(EditProducts.ImageName))
                {
                    string oldFilePath = Path.Combine(uploadsFolder, EditProducts.ImageName);

                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }
                string extension = Path.GetExtension(EditProducts.imgUp.FileName);
                string newFileName = $"{EditProducts.Id}{extension}";
                string newFilePath = Path.Combine(uploadsFolder, newFileName);
                //string filePath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "AdminPanel", "Photo", "Products", EditProducts.Id
                //    + ".png");
                //if(System.IO.File.Exists(filePath2))
                //    System.IO.File.Delete(filePath2);

                //string filePath3 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "AdminPanel", "Photo", "Products", EditProducts.Id
                //    + ".jpg");
                //if (System.IO.File.Exists(filePath3))
                //    System.IO.File.Delete(filePath3);

                //string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "AdminPanel", "Photo", "Products", EditProducts.Id
                //    + Path.GetExtension(EditProducts.imgUp.FileName));
                using (var stream = new FileStream(newFilePath, FileMode.Create))
                {
                    EditProducts.imgUp.CopyTo(stream);
                }
                EditProducts.ImageName = EditProducts.Id + Path.GetExtension(EditProducts.imgUp.FileName);
            }
            await _productsServices.EditProdutcs(EditProducts);
            return RedirectToPage("index");
        }
    }
}
