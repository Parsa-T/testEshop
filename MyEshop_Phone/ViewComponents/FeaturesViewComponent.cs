using Microsoft.AspNetCore.Mvc;
using MyEshop_Phone.Domain.Interface;

public class FeaturesViewComponent : ViewComponent
{
    IProductsFeaturseRepository _productsFeaturseRepository;
    public FeaturesViewComponent(IProductsFeaturseRepository products)
    {
        _productsFeaturseRepository = products;
    }
    public async Task<IViewComponentResult> InvokeAsync(int id)
    {
        var model = await _productsFeaturseRepository.ShowfeatureById(id);
        return View(model);
    }
}
