using Microsoft.AspNetCore.Mvc;
using MyEshop_Phone.Application.Interface;

namespace MyEshop_Phone.Controllers
{
    public class SearchController : Controller
    {
        IProductsServices _productsService;
        public SearchController(IProductsServices products)
        {
            _productsService = products;
        }
        public async Task<IActionResult> Index(string q)
        {
            ViewBag.Name = q;
            return View(await _productsService.ReSearch(q));
        }
    }
}
