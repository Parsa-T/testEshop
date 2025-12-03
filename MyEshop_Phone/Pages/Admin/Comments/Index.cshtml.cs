using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Domain.Interface;
using MyEshop_Phone.Domain.Model;

namespace MyEshop_Phone.Pages.Admin.Comments
{
    public class IndexModel : PageModel
    {
        IProductsCommentRepository _productsCommentRepository;
        public IndexModel(IProductsCommentRepository products)
        {
            _productsCommentRepository = products;
        }
        public IEnumerable<_Products_comment> AllComment { get; set; }
        public async Task OnGet()
        {
            AllComment = await _productsCommentRepository.ShowComments();
        }
    }
}
