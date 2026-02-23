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
            if (EditProducts.imgUp != null && EditProducts.imgUp.Length > 0)
            {
                string uploadsFolder = Path.Combine(
                    _env.WebRootPath,
                    "AdminPanel",
                    "Photo",
                    "Products"
                );

                Directory.CreateDirectory(uploadsFolder);

                // حذف عکس قدیمی
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

                using (var stream = new FileStream(newFilePath, FileMode.Create))
                {
                    await EditProducts.imgUp.CopyToAsync(stream);
                }

                // فقط وقتی عکس آپلود شد این مقدار آپدیت شود
                EditProducts.ImageName = newFileName;
            }

            await _productsServices.EditProdutcs(EditProducts);

            return RedirectToPage("index");
        }
    }
}
