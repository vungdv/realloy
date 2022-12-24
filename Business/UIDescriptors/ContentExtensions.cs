using EPiServer.Filters;
using EPiServer.Framework.Web;
using EPiServer.ServiceLocation;

namespace realloy.Business.UIDescriptors;

public static class ContentExtensions
{
    public static IEnumerable<T> FilterForDisplay<T>(
        this IEnumerable<T> contents,
        bool requirePageTemplate = true,
        bool requireVisibleInMenu = true
        ) where T : IContent
    {
        var accessFilter = new FilterAccess();
        var publishedFilter = new FilterPublished();
        contents = contents.Where(x => !publishedFilter.ShouldFilter(x) && !accessFilter.ShouldFilter(x));
        if (requirePageTemplate)
        {
            var templateFilter = ServiceLocator.Current.GetInstance<FilterTemplate>();
            templateFilter.TemplateTypeCategories = TemplateTypeCategories.Request;
            contents = contents.Where(x => !templateFilter.ShouldFilter(x));
        }
        if (requireVisibleInMenu)
        {
            contents = contents.Where(x => x is PageData pageData && pageData.VisibleInMenu);
        }
        return contents;
    }
}