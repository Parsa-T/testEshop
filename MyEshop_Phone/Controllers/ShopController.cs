using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using MyEshop_Phone.Application.ViewModel;
using System.Text.Json;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyEshop_Phone.Domain.Model;

namespace MyEshop_Phone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        IOrderServices _orderServices;
        IPaymentGateway _paymentGateway;
        public ShopController(IOrderServices order,IPaymentGateway payment)
        {
            _orderServices = order;
            _paymentGateway = payment;
        }
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
        public int Get(int id, string? color)
        {
            List<ShopCartItem> list = new List<ShopCartItem>();

            // گرفتن Session
            var shopCartJson = HttpContext.Session.GetString("ShopCart");

            if (!string.IsNullOrEmpty(shopCartJson))
            {
                list = JsonSerializer.Deserialize<List<ShopCartItem>>(shopCartJson)!;
            }

            // بررسی وجود محصول در سبد
            var existingItem = list.FirstOrDefault(p =>
                                                   p.ProductID == id &&
                                                   (p.ColorName ?? "") == (color ?? ""));
            if (existingItem != null)
            {
                existingItem.Count += 1;
            }
            else
            {
                list.Add(new ShopCartItem
                {
                    ProductID = id,
                    Count = 1,
                    ColorName = string.IsNullOrEmpty(color) ? null : color,
                });
            }

            // ذخیره دوباره در Session
            HttpContext.Session.SetString("ShopCart", JsonSerializer.Serialize(list));

            // برگرداندن مجموع تعداد آیتم‌ها
            return list.Sum(l => l.Count);
        }
        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            var shopCartJson = HttpContext.Session.GetString("ShopCart");
            if (string.IsNullOrEmpty(shopCartJson))
                return Ok(0);

            var list = JsonSerializer.Deserialize<List<ShopCartItem>>(shopCartJson);

            // حذف محصول
            list.RemoveAll(p => p.ProductID == id);

            // ذخیره دوباره Session
            HttpContext.Session.SetString("ShopCart", JsonSerializer.Serialize(list));

            // برگرداندن مجموع تعداد آیتم‌ها
            return Ok(list.Sum(x => x.Count));
        }
        [HttpPost("pay/{orderId}")]
        public async Task<IActionResult> Pay(int orderId)
        {
            var order =await _orderServices.FinIdOrder(orderId);
            if (order == null || order.IsFinaly)
                return BadRequest();
            var authority = await _paymentGateway.RequestPaymentAsync(order.Id, order.TotalPrice);
            if(authority==null)
                return BadRequest("خطا در درگاه");
            order.Authority = authority;
            await _orderServices.SaveAsync();
            var url = $"https://sandbox.zarinpal.com/pg/StartPay/{authority}";
            return Redirect(url);

        }
        [HttpGet("verify")]
        public async Task<IActionResult> Verify(string Authority, string Status)
        {
            if (Status != "OK")
                return BadRequest("پرداخت لغو شد");

            var order = await _orderServices.FindOrderByAuthorityAsync(Authority);

            if (order == null)
                return NotFound();

            var result = await _paymentGateway
                .VerifyAsync(Authority, order.TotalPrice);

            if (!result.IsSuccess)
                return Redirect("/PaymentResult/failed");

            order.IsFinaly = true;
            order.RefId = result.RefId;
            order.Date = DateTime.Now;

            await _orderServices.SaveAsync();
            HttpContext.Session.Remove("ShopCart");
            return Redirect("/PaymentResult/Success?refId=" + order.RefId);
        }

    }
}
