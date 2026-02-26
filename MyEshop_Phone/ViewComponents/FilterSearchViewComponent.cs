using Microsoft.AspNetCore.Mvc;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Domain.Model;

public class FilterSearchViewComponent : ViewComponent
{
    IProductsServices _productsService;
    public FilterSearchViewComponent(IProductsServices products)
    {
        _productsService = products;
    }
    public async Task<IViewComponentResult> InvokeAsync(IEnumerable<_Products>? products = null)
    {
        if (products != null)
            return View(products);

        var allProducts = await _productsService.GetAllProduct();
        return View(allProducts);
    }
}
