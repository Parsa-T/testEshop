using Microsoft.AspNetCore.Mvc;

namespace MyEshop_Phone.Controllers
{
    public class PaymentResultController : Controller
    {
        public IActionResult success(long RefId)
        {
            ViewBag.RefId = RefId;
            return View();
        }
        public IActionResult failed()
        {
            return View();
        }
    }
}
