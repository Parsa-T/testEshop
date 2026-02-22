using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyEshop_Phone.Application.Common.Interfaces;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Application.ViewModel;
using MyEshop_Phone.Domain.Interface;
using MyEshop_Phone.Domain.Model;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyEshop_Phone.Controllers
{
    public class ShopCartController : Controller
    {
        IProductsServices _productsService;
        IUserRepository _userRepository;
        IOrderServices _orderServices;
        IPaymentGateway _paymentGateway;
        IOrderDitelsServices _orderDitelsService;
        public ShopCartController(IProductsServices products,IUserRepository user, IOrderServices orderServices, IPaymentGateway paymentGateway, IOrderDitelsServices orderDitelsService)
        {
            _productsService = products;
            _userRepository = user;
            _orderServices = orderServices;
            _paymentGateway = paymentGateway;
            _orderDitelsService = orderDitelsService;
        }
        public async Task<IActionResult> Index()
        {
            List<ShowOrderViewModel> model = new();
            var shopCartJson = HttpContext.Session.GetString("ShopCart");
            if (!string.IsNullOrEmpty(shopCartJson))
            {
                var cartItems = JsonSerializer.Deserialize<List<ShopCartItem>>(shopCartJson);
                foreach (var item in cartItems)
                {
                    var product = await _productsService.ShopCartItem(item.ProductID);

                    if (product != null)
                    {
                        model.Add(new ShowOrderViewModel
                        {
                            ProductID = product.Id,
                            Title = product.Title,
                            ImageName = product.ImageName,
                            Count = item.Count,
                            Price = product.Price,
                            Sum = product.Price * item.Count,
                            ShortDescription = product.ShortDescription,
                            ColorName = item.ColorName,
                        });
                    }
                }
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return RedirectToAction("Login");
            var user = await _userRepository.FindUserById(int.Parse(userId));
            ViewBag.HasAddress = !string.IsNullOrWhiteSpace(user.Address) &&
                !string.IsNullOrWhiteSpace(user.PostalCode.ToString());
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> FinalizeOrder()
        {
            var shopCartJson = HttpContext.Session.GetString("ShopCart");

            if (string.IsNullOrEmpty(shopCartJson))
                return BadRequest("سبد خرید خالی است");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return RedirectToAction("Login");

            var cartItems = JsonSerializer.Deserialize<List<ShopCartItem>>(shopCartJson);

            decimal totalPrice = 0;

            foreach (var item in cartItems)
            {
                var product = await _productsService.ShopCartItem(item.ProductID);
                totalPrice += product.Price * item.Count;
            }

            // 🔥 ساخت سفارش
            var order = new _Orders
            {
                UserId = int.Parse(userId),
                Date = DateTime.Now,
                IsFinaly = false,
                TotalPrice = totalPrice
            };
            if(order.IsFinaly)
                return BadRequest("سفارش قبلا ثبت شده");

            await _orderServices.AddOrderAsunc(order);
            await _orderServices.SaveAsync();
            // 🔥 ساخت OrderDetails
            foreach (var item in cartItems)
            {
                var product = await _productsService.ShopCartItem(item.ProductID);

                if (product == null) continue;

                var detail = new _OrderDetails
                {
                    OrderId = order.Id,
                    ProductId = product.Id,
                    Count = item.Count,
                    Price = product.Price,
                    ColorName = item.ColorName,
                    ProductTitle = product.Title
                };
                await _orderDitelsService.AddDitelsAsync(detail);
                if (product.Count != 0)
                {
                    product.Count -= detail.Count;
                    await _productsService.Save();
                }
            }

            await _orderServices.SaveAsync();

            // 🔥 رفتن به پرداخت
            var authority = await _paymentGateway
                .RequestPaymentAsync(order.Id, order.TotalPrice);

            if (authority == null)
                return BadRequest("خطا در اتصال به درگاه");

            order.Authority = authority;
            await _orderServices.SaveAsync();

            var url = $"https://zarinpal.com/pg/StartPay/{authority}";
            //var url = $"https://sandbox.zarinpal.com/pg/StartPay/{authority}";
            return Redirect(url);
        }
    }
}
