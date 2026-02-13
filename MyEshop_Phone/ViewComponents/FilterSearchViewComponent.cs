using Microsoft.AspNetCore.Mvc;
using MyEshop_Phone.Application.Interface;

public class FilterSearchViewComponent : ViewComponent
{
    IProductsServices _productsService;
    public FilterSearchViewComponent(IProductsServices products)
    {
        _productsService = products;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var products = await _productsService.GetAllProduct();
        return View(products);
    }
}
