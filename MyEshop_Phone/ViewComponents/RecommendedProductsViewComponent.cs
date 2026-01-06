using Microsoft.AspNetCore.Mvc;
using MyEshop_Phone.Application.Interface;

public class RecommendedProductsViewComponent : ViewComponent
{
    IProductsServices _productsService;
    public RecommendedProductsViewComponent(IProductsServices products)
    {
        _productsService = products;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var model = await _productsService.ShowRecommendedProducts();
        return View(model);
    }
}

