using EPiServer.Web;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace realloy.Extensions;

public static class ViewContextExtension
{
    public static bool IsPreviewMode(this ViewContext viewContext)
    {
        return viewContext.IsInEditMode()
            && (viewContext.ActionDescriptor as ControllerActionDescriptor)?.ControllerName == "Preview";
    }

    public static bool IsInEditMode(this ViewContext viewContext)
    {
        var mode = viewContext.HttpContext.RequestServices.GetRequiredService<IContextModeResolver>().CurrentMode;
        return mode is ContextMode.Edit or ContextMode.Preview;
    }
}