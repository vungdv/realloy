using System.Text;
using System.Text.Encodings.Web;
using EPiServer.ServiceLocation;
using EPiServer.Web.Mvc.Html;
using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using realloy.Business.UIDescriptors;

namespace realloy.Helpers;

public static class HtmlHelpers
{
    public static IHtmlContent MenuList(
        this IHtmlHelper helper,
        ContentReference rootLink,
        Func<MenuItem, IHtmlContent> itemTemplate = null,
        bool includeRoot = false,
        bool requireVisibleInMenu = true,
        bool requirePageTemplate = true
        )
    {
        itemTemplate ??= DefaultTemplate(helper);
        var currentContentLink = helper.ViewContext.HttpContext.GetContentLink();
        var contentLoader = helper.ViewContext.HttpContext.RequestServices.GetService<IContentLoader>();
        IEnumerable<PageData> filter(IEnumerable<PageData> pages)
            => pages.FilterForDisplay(requirePageTemplate, requireVisibleInMenu);

        var pagePath = contentLoader.GetAncestors(currentContentLink)
            .Reverse()
            .Select(page => page.ContentLink)
            .SkipWhile(page => !page.CompareToIgnoreWorkID(rootLink))
            .ToList();

        var menuItems = contentLoader.GetChildren<PageData>(rootLink)
            .FilterForDisplay(requirePageTemplate, requireVisibleInMenu)
            .Select(page => CreateMenuItem(page, currentContentLink, contentLoader, pagePath, filter))
            .ToList();

        if (includeRoot)
        {
            menuItems.Insert(0, CreateMenuItem(
                contentLoader.Get<PageData>(rootLink),
                currentContentLink,
                contentLoader,
                pagePath,
                filter));
        }

        var buffer = new StringBuilder();
        var writer = new StringWriter(buffer);
        foreach (var menuItem in menuItems)
        {
            itemTemplate(menuItem).WriteTo(writer, HtmlEncoder.Default);
        }

        return new HtmlString(buffer.ToString());
    }
    private static MenuItem CreateMenuItem(
        PageData page,
        ContentReference currentContentLink,
        IContentLoader contentLoader,
        List<ContentReference> pagePath,
        Func<IEnumerable<PageData>, IEnumerable<PageData>> filter)
    {
        return new MenuItem(page)
        {
            Selected = page.ContentLink.CompareToIgnoreWorkID(currentContentLink) ||
                            pagePath.Contains(page.ContentLink),
            HasChildren = new Lazy<bool>(() => filter(contentLoader.GetChildren<PageData>(page.ContentLink)).Any())
        };
    }
    private static Func<MenuItem, HelperResult> DefaultTemplate(IHtmlHelper helper)
    {
        return x => new HelperResult(writer =>
        {
            helper.PageLink(x.Page).WriteTo(writer, HtmlEncoder.Default);
            return Task.CompletedTask;
        });
    }

    public class MenuItem
    {
        public MenuItem(PageData page)
        {
            Page = page;
        }

        public PageData Page { get; set; }
        public bool Selected { get; set; }
        public Lazy<bool> HasChildren { get; set; }
    }
}