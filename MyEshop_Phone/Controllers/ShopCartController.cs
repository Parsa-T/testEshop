using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Application.ViewModel;
using MyEshop_Phone.Domain.Interface;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyEshop_Phone.Controllers
{
    public class ShopCartController : Controller
    {
        IProductsServices _productsService;
        IUserRepository _userRepository;
        public ShopCartController(IProductsServices products,IUserRepository user)
        {
            _productsService = products;
            _userRepository = user;
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
    }
}
