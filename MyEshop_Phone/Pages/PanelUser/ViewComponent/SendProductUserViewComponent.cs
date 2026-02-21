
using Microsoft.AspNetCore.Mvc;

public class SendProductUserViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}
