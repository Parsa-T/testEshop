using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Domain.Interface;
using MyEshop_Phone.Domain.Model;

namespace MyEshop_Phone.Pages.Admin.Comments
{
    public class DeleteModel : PageModel
    {
        IProductsCommentRepository _productsCommentRepository;
        public DeleteModel(IProductsCommentRepository products)
        {
            _productsCommentRepository = products;
        }
        [BindProperty]
        public _Products_comment DeleteComment { get; set; }
        public async Task OnGet(int id)
        {
            DeleteComment = await _productsCommentRepository.GetCommentById(id);
        }
        public async Task<IActionResult> OnPost()
        {
            var comment = await _productsCommentRepository.GetCommentById(DeleteComment.Id);
            _productsCommentRepository.DeleteComments(comment);
            await _productsCommentRepository.Save();
            return RedirectToPage("index");
        }
    }
}
