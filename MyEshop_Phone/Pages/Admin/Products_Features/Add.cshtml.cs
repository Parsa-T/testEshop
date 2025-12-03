using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Domain.Model;
using static MyEshop_Phone.Application.DTO.GetFeaturseAndProductsFeaturseDTO;

namespace MyEshop_Phone.Pages.Admin.Products_Features
{
    public class AddModel : PageModel
    {
        IProductsFeaturseServices _productsFeaturseServices;
        IFeaturesServices _featuresServices;
        IProductsServices _productsServices;
        public AddModel(IProductsFeaturseServices productsFeaturseServices, IFeaturesServices featuresServices,IProductsServices productsServices)
        {
            _productsFeaturseServices = productsFeaturseServices;
            _featuresServices = featuresServices;
            _productsServices = productsServices;
        }
        [BindProperty]
        public AddFeaturesAndProductsFeaturesDTO AddFeatures { get; set; }
        public async Task OnGet()
        {
            AddFeatures = new AddFeaturesAndProductsFeaturesDTO
            {
                features = await _featuresServices.GetAllAsync(),
                products = await _productsServices.GetProductsDropDown()
            };
        }
        public async Task<IActionResult> OnPost()
        {
            ModelState.Remove("AddFeatures.features");
            ModelState.Remove("AddFeatures.products");
            if (!ModelState.IsValid)
            {
                AddFeatures.features = await _featuresServices.GetAllAsync();
                AddFeatures.products = await _productsServices.GetProductsDropDown();
                return Page();
            }
            await _productsFeaturseServices.AddFeaturse(AddFeatures);
            return RedirectToPage("Ditlse", new { id = AddFeatures.ProductsId });
        }
    }
}
