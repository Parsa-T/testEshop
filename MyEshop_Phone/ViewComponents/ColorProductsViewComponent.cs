using Microsoft.AspNetCore.Mvc;
using MyEshop_Phone.Domain.Interface;

public class ColorProductsViewComponent : ViewComponent
{
    IProductColorRepository _productColorRepository;
    public ColorProductsViewComponent(IProductColorRepository product)
    {
        _productColorRepository = product;
    }
    public async Task<IViewComponentResult> InvokeAsync(int id)
    {
        var model = await _productColorRepository.ShowColorByID(id);
        return View(model);
    }
}

