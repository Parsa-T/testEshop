using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.IO;
using System.Threading.Tasks;

public static class PartialToStringClass
{
    public static async Task<string> RenderAsync<TModel>(
        Controller controller,
        string partialViewName,
        TModel model)
    {
        if (string.IsNullOrEmpty(partialViewName))
            partialViewName = controller.ControllerContext.ActionDescriptor.ActionName;

        controller.ViewData.Model = model;

        using var writer = new StringWriter();

        var viewEngine = controller.HttpContext
            .RequestServices
            .GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;

        var viewResult = viewEngine.FindView(
            controller.ControllerContext,
            partialViewName,
            isMainPage: false);

        if (!viewResult.Success)
            throw new FileNotFoundException(
                $"Partial view '{partialViewName}' not found.");

        var viewContext = new ViewContext(
            controller.ControllerContext,
            viewResult.View,
            controller.ViewData,
            controller.TempData,
            writer,
            new HtmlHelperOptions()
        );

        await viewResult.View.RenderAsync(viewContext);

        return writer.ToString();
    }
}
