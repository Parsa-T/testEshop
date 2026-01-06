using Microsoft.AspNetCore.Mvc;
using MyEshop_Phone.Application.Interface;

public class ProtectionPhoneViewComponent : ViewComponent
{
    IProductsServices _productsServices;
    public ProtectionPhoneViewComponent(IProductsServices products)
    {
        _productsServices = products;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var model = await _productsServices.ShowProtectionPhone();
        return View(model);
    }
}
