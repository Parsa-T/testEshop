using Microsoft.AspNetCore.Mvc;
using MyEshop_Phone.Application.Interface;

namespace MyEshop_Phone.Controllers
{
    public class FilterController : Controller
    {
        IProductsServices _productsService;
        public FilterController(IProductsServices products)
        {
            _productsService = products;
        }
        [Route("Filter/{filterName}")]
        public async Task<IActionResult> Filter(string filterName)
        {
            ViewData["FilterName"] = filterName;
            return View(await _productsService.FilterByNameProducts(filterName));
        }
    }
}
