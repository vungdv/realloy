using EPiServer.Data;
using EPiServer.Web;
using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using realloy.Models.Pages;
using realloy.Models.ViewModels;

namespace realloy.Business.UIDescriptors;

public class PageContextActionFilter : IResultFilter
{
    private readonly IContentLoader _contentLoader;
    private readonly IDatabaseMode _databaseMode;

    public PageContextActionFilter(IContentLoader contentLoader, IDatabaseMode databaseMode)
    {
        _contentLoader = contentLoader;
        _databaseMode = databaseMode;
    }

    public void OnResultExecuting(ResultExecutingContext context)
    {
        var controller = context.Controller as Controller;
        var viewModel = controller?.ViewData.Model;

        if (viewModel is IPageViewModel<SitePageData> model)
        {
            var currentContentLink = context.HttpContext.GetContentLink();
            model.Layout = new LayoutModel
            {
                IsInReadonlyMode = _databaseMode.DatabaseMode == DatabaseMode.ReadOnly
            };
            model.Section ??= ((SitePageData)GetSection(currentContentLink));
        }
    }
    private IContent GetSection(ContentReference contentLink)
    {
        var currentContent = _contentLoader.Get<IContent>(contentLink);

        static bool isSectionRoot(ContentReference contentReference) =>
           ContentReference.IsNullOrEmpty(contentReference) ||
           contentReference.Equals(SiteDefinition.Current.StartPage) ||
           contentReference.Equals(SiteDefinition.Current.RootPage);

        if (isSectionRoot(currentContent.ParentLink))
        {
            return currentContent;
        }

        return _contentLoader
            .GetAncestors(contentLink)
            .OfType<PageData>()
            .SkipWhile(x => !isSectionRoot(x.ParentLink))
            .FirstOrDefault();
    }

    public void OnResultExecuted(ResultExecutedContext context)
    {
    }
}