using EPiServer.Framework.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using realloy.Models.Pages;
using realloy.Models.ViewModels;

namespace realloy.Controllers;

[TemplateDescriptor(Inherited = true)]
public class DefaultPageController : PageControllerBase<SitePageData>
{
    [HttpGet]
    public IActionResult Index(SitePageData currentPage)
    {
        var model = CreateModel(currentPage);
        return View($"~/Views/{currentPage.GetOriginalType().Name}/Index.cshtml", model);
    }

    private static IPageViewModel<SitePageData>? CreateModel(SitePageData page)
    {
        var type = typeof(PageViewModel<>).MakeGenericType(page.GetOriginalType());
        return Activator.CreateInstance(type, page) as IPageViewModel<SitePageData>;
    }
}