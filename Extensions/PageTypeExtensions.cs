using EPiServer.ServiceLocation;

namespace realloy.Extensions;

public static class PageTypeExtensions
{
    public static PageType GetPageType(this Type pageType)
    {
        var pageTypeRepository = ServiceLocator.Current.GetInstance<IContentTypeRepository<PageType>>();
        return pageTypeRepository.Load(pageType);
    }
}