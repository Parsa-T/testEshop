using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyEshop_Phone.Pages
{
    public class ErrorModel : PageModel
    {
        public string ErrorMessage { get; set; }

        public void OnGet()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exception != null)
            {
                ErrorMessage = exception.Error.Message;
            }
        }
    }
}
