using Microsoft.AspNetCore.Mvc;
using MyEshop_Phone.Application.Interface;

public class HeaderMenuViewComponent : ViewComponent
{
    private readonly IGroupsSubMenuServices _services;

    public HeaderMenuViewComponent(IGroupsSubMenuServices services)
    {
        _services = services;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var model = await _services.ShowAll();
        return View(model);
    }
}
