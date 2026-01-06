using Microsoft.AspNetCore.Mvc;
using MyEshop_Phone.Application.Interface;

public class BestSellerViewComponent : ViewComponent
{
    IProductsServices _productsService;
    public BestSellerViewComponent(IProductsServices products)
    {
        _productsService = products;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var model = await _productsService.BestSeller();
        return View(model);
    }
}
