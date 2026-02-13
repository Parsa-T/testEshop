using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using MyEshop_Phone.Application.ViewModel;
using System.Text.Json;

namespace MyEshop_Phone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        public int Get()
        {
            // لیست پیش‌فرض
            List<ShopCartItem> list = new List<ShopCartItem>();

            // گرفتن رشته JSON از Session
            var shopCartJson = HttpContext.Session.GetString("ShopCart");

            if (!string.IsNullOrEmpty(shopCartJson))
            {
                // تبدیل JSON به لیست
                list = JsonSerializer.Deserialize<List<ShopCartItem>>(shopCartJson)!;
            }

            // مجموع تعداد آیتم‌ها
            return list.Sum(l => l.Count);
        }

        // افزودن یا افزایش آیتم در سبد خرید
        [HttpGet("{id}")]
        public int Get(int id)
        {
            List<ShopCartItem> list = new List<ShopCartItem>();

            // گرفتن Session
            var shopCartJson = HttpContext.Session.GetString("ShopCart");

            if (!string.IsNullOrEmpty(shopCartJson))
            {
                list = JsonSerializer.Deserialize<List<ShopCartItem>>(shopCartJson)!;
            }

            // بررسی وجود محصول در سبد
            var existingItem = list.FirstOrDefault(p => p.ProductID == id);
            if (existingItem != null)
            {
                existingItem.Count += 1;
            }
            else
            {
                list.Add(new ShopCartItem
                {
                    ProductID = id,
                    Count = 1
                });
            }

            // ذخیره دوباره در Session
            HttpContext.Session.SetString("ShopCart", JsonSerializer.Serialize(list));

            // برگرداندن مجموع تعداد آیتم‌ها
            return Get();
        }
    }
}
