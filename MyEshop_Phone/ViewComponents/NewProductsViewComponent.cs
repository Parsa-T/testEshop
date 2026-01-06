
using Microsoft.AspNetCore.Mvc;
using MyEshop_Phone.Application.Interface;

public class NewProductsViewComponent : ViewComponent
{
    IProductsServices _productsServices;
    public NewProductsViewComponent(IProductsServices products)
    {
        _productsServices = products;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var model = await _productsServices.BestSeller();
        return View(model);
    }
}
