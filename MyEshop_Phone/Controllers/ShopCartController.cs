using MediatR;
using Microsoft.AspNetCore.Authorization;
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
            var viewModel = new ShopCartPageViewModel();

            var shopCartJson = HttpContext.Session.GetString("ShopCart");

            if (!string.IsNullOrEmpty(shopCartJson))
            {
                var cartItems = JsonSerializer.Deserialize<List<ShopCartItem>>(shopCartJson);

                foreach (var item in cartItems)
                {
                    var product = await _productsService.ShopCartItem(item.ProductID);

                    if (product != null)
                    {
                        viewModel.Items.Add(new ShowOrderViewModel
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

            if (userId != null)
            {
                viewModel.IsLoggedIn = true;

                var user = await _userRepository.FindUserById(int.Parse(userId));

                viewModel.FullName = $"{user.Name}   {user.Family}";
                viewModel.Address = user.Address;
                viewModel.PostalCode = user.PostalCode.ToString();
            }
            else
            {
                viewModel.IsLoggedIn = false;
            }

            return View(viewModel);
        }
        [HttpPost]
        [Authorize] // 👈 مهم
        public async Task<IActionResult> FinalizeOrder()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Login", "Account");

            var shopCartJson = HttpContext.Session.GetString("ShopCart");

            if (string.IsNullOrEmpty(shopCartJson))
                return RedirectToAction("Index");

            var cartItems = JsonSerializer.Deserialize<List<ShopCartItem>>(shopCartJson);

            if (cartItems == null || !cartItems.Any())
                return RedirectToAction("Index");

            decimal totalPrice = 0;

            foreach (var item in cartItems)
            {
                var product = await _productsService.ShopCartItem(item.ProductID);

                if (product == null)
                    return BadRequest("یکی از محصولات یافت نشد");

                if (product.Count < item.Count)
                    return BadRequest($"موجودی محصول {product.Title} کافی نیست");

                totalPrice += product.Price * item.Count;
            }

            var order = new _Orders
            {
                UserId = int.Parse(userId),
                Date = DateTime.Now,
                IsFinaly = false,
                TotalPrice = totalPrice
            };

            await _orderServices.AddOrderAsunc(order);
            await _orderServices.SaveAsync();

            foreach (var item in cartItems)
            {
                var product = await _productsService.ShopCartItem(item.ProductID);

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

                product.Count -= item.Count;
                await _productsService.Save();
            }

            await _orderServices.SaveAsync();

            var authority = await _paymentGateway
                .RequestPaymentAsync(order.Id, order.TotalPrice);

            if (string.IsNullOrEmpty(authority))
                return BadRequest("خطا در اتصال به درگاه");

            order.Authority = authority;
            await _orderServices.SaveAsync();

            // 🔥 پاک کردن سشن بعد از رفتن به پرداخت
            HttpContext.Session.Remove("ShopCart");

            var url = $"https://zarinpal.com/pg/StartPay/{authority}";
            return Redirect(url);
        }
    }
}
