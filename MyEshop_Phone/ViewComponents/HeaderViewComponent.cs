using Microsoft.AspNetCore.Mvc;
using MyEshop_Phone.Application.Interface;

public class HeaderViewComponent : ViewComponent
{
    private readonly IGroupsSubMenuServices _services;

    public HeaderViewComponent(IGroupsSubMenuServices services)
    {
        _services = services;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var model = await _services.ShowAll();
        return View(model);
    }
}
