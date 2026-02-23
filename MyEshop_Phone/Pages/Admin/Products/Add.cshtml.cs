using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Domain.Model;

namespace MyEshop_Phone.Pages.Admin.Products
{
    public class AddModel : PageModel
    {
        IProductsServices _productsServices;
        private readonly IWebHostEnvironment _env;
        public AddModel(IProductsServices products, IWebHostEnvironment env)
        {
            _productsServices = products;
            _env = env;
        }
        [BindProperty]
        public ShowProductsDTO AddProduct { get; set; }
        public async Task OnGet()
        {
            AddProduct = await _productsServices.GetAllProducts();
        }
        public async Task<IActionResult> OnPost()
        {
            var product = new _Products
            {
                CreateTime = DateTime.Now,
                Price = AddProduct.Price,
                Text = AddProduct.Text,
                Title = AddProduct.Title,
                ShortDescription = AddProduct.ShortDescription,
                ProductGroupsId = AddProduct.ProductGroupsId,
                Count = AddProduct.Count,
                SubmenuGroupsId = AddProduct.SubmenuGroupsId,
                RecommendedProducts = AddProduct.RecommendedProducts,
            };
            await _productsServices.RegisterProducts(product);
            await _productsServices.Save();
            if (AddProduct.imgUp?.Length > 0 && AddProduct.imgUp != null)
            {
                //string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "AdminPanel", "Photo", "Products", product.Id
                //    + Path.GetExtension(AddProduct.imgUp.FileName));
                string uploadsFolder = Path.Combine(
_env.WebRootPath,
"AdminPanel",
"Photo",
"Products"
);
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                string fileName = product.Id + Path.GetExtension(AddProduct.imgUp.FileName);
                string filePath = Path.Combine(uploadsFolder, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    AddProduct.imgUp.CopyTo(stream);
                }
                product.ImageName = product.Id + Path.GetExtension(AddProduct.imgUp.FileName);
                await _productsServices.Save();
            }
            return RedirectToPage("index");
        }
    }
}
