using EPiServer.Data;
using EPiServer.Web;
using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using realloy.Models.Pages;
using realloy.Models.ViewModels;

namespace realloy.Business.UIDescriptors;

public class PageContextActionFilter : IResultFilter
{
    private readonly IContentLoader _contentLoader;
    private readonly IDatabaseMode _databaseMode;
    private readonly IUrlResolver _urlResolver;

    public PageContextActionFilter(
        IContentLoader contentLoader,
        IDatabaseMode databaseMode,
        IUrlResolver urlResolver)
    {
        _contentLoader = contentLoader;
        _databaseMode = databaseMode;
        _urlResolver = urlResolver;
    }

    public void OnResultExecuting(ResultExecutingContext context)
    {
        var controller = context.Controller as Controller;
        var viewModel = controller?.ViewData.Model;

        if (viewModel is IPageViewModel<SitePageData> model)
        {
            var currentContentLink = context.HttpContext.GetContentLink();
            var startPageContentLink = SiteDefinition.Current.StartPage;

            // Use the content link with version information when editing the startpage,
            // otherwise the published version will be used when rendering the props below.
            if (currentContentLink.CompareToIgnoreWorkID(startPageContentLink))
            {
                startPageContentLink = currentContentLink;
            }

            var startPage = _contentLoader.Get<StartPage>(startPageContentLink);

            model.Layout = new LayoutModel
            {
                LogoType = startPage.SiteLogoType,
                ProductPages = startPage.ProductPageLinks,
                CompanyInformationPages = startPage.CompanyInformationPageLinks,
                NewsPages = startPage.NewsPageLinks,
                CustomerZonePages = startPage.CustomerZonePageLinks,
                LogoTypeLinkUrl = new HtmlString(_urlResolver.GetUrl(startPageContentLink)),
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