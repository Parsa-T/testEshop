using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Infra.Data.Context;

namespace MyEshop_Phone.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        IUserServices _userservices;
        IProductsServices _productservices;
        IOrderServices _orderservices;
        public int UserCount { get; set; }
        public int ProductsCount { get; set; }
        public int OrderCount { get; set; }
        public IndexModel(IUserServices user, IProductsServices products,IOrderServices orderservices)
        {
            _userservices = user;
            _productservices = products;
            _orderservices = orderservices;
            _orderservices = orderservices;
        }
        public async Task OnGet()
        {
            UserCount = await _userservices.GetCountUser();
            ProductsCount = await _productservices.GetProductsCount();
            OrderCount = await _orderservices.CountOrderAsync();
        }
    }
}
