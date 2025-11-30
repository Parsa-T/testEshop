using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Application.Interface;

namespace MyEshop_Phone.Pages.Admin.Products
{
    public class EditModel : PageModel
    {
        IProductsServices _productsServices;
        public EditModel(IProductsServices productsServices)
        {
            _productsServices = productsServices;
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
            if (EditProducts.imgUp?.Length > 0)
            {
                string filePath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "AdminPanel", "Photo", "Products", EditProducts.Id
                    + ".png");
                if(System.IO.File.Exists(filePath2))
                    System.IO.File.Delete(filePath2);

                string filePath3 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "AdminPanel", "Photo", "Products", EditProducts.Id
                    + ".jpg");
                if (System.IO.File.Exists(filePath3))
                    System.IO.File.Delete(filePath3);

                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "AdminPanel", "Photo", "Products", EditProducts.Id
                    + Path.GetExtension(EditProducts.imgUp.FileName));
                using (var stream = new FileStream(filePath,FileMode.Create))
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
