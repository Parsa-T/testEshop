using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Domain.Interface;
using static MyEshop_Phone.Application.DTO.GetFeaturseAndProductsFeaturseDTO;

namespace MyEshop_Phone.Pages.Admin.Products_Features
{
    public class DitlseModel : PageModel
    {
        IProductsFeaturseServices _productsFeaturseServices;
        IProductsFeaturseRepository _productsFeaturseRepository;
        public DitlseModel(IProductsFeaturseServices productsFeaturseServices,IProductsFeaturseRepository products)
        {
            _productsFeaturseServices = productsFeaturseServices;
            _productsFeaturseRepository = products;
        }
        [BindProperty]
        public ProductDetailsDTO product { get; set; }
        public async Task OnGet(int id)
        {
            product = await _productsFeaturseServices.GetProductDetails(id);
        }

        public async Task<IActionResult> OnPostDeleteAsync(int featureId)
        {
            var result = await _productsFeaturseRepository.DeleteProductsFeatures(featureId);

            if (!result)
                return new JsonResult(new { success = false, message = "Record not found" });

            return new JsonResult(new { success = true });
        }
    }
}
