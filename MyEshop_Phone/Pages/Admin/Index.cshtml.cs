using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Infra.Data.Context;

namespace MyEshop_Phone.Pages.Admin
{
    public class IndexModel : PageModel
    {
        IUserServices _userservices;
        IProductsServices _productservices;
        IProductsCommentServices _productscommentservices;
        public int UserCount { get; set; }
        public int ProductsCount { get; set; }
        public int CommentCount { get; set; }
        public IndexModel(IUserServices user, IProductsServices products, IProductsCommentServices commentServices)
        {
            _userservices = user;
            _productservices = products;
            _productscommentservices = commentServices;
        }
        public async Task OnGet()
        {
            UserCount = await _userservices.GetCountUser();
            ProductsCount = await _productservices.GetProductsCount();
            CommentCount = await _productscommentservices.GetCommentCount();
        }
    }
}
