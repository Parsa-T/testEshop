using Microsoft.AspNetCore.Mvc;

namespace MyEshop_Phone.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddShop(int shopId)
        {
            return View();
        }
    }
}
