using System.Globalization;
using EPiServer.Filters;
using EPiServer.ServiceLocation;

namespace realloy.Business;

[ServiceConfiguration(Lifecycle = ServiceInstanceScope.Singleton)]
public class ContentLocator
{
    private readonly IContentLoader _contentLoader;
    private readonly IContentProviderManager _contentProviderManager;
    private readonly IPageCriteriaQueryService _pageCriteriaQueryService;
    public ContentLocator(IContentLoader contentLoader, IContentProviderManager contentProviderManager, IPageCriteriaQueryService pageCriteriaQueryService)
    {
        _contentLoader = contentLoader;
        _contentProviderManager = contentProviderManager;
        _pageCriteriaQueryService = pageCriteriaQueryService;
    }

    /// <summary>
    /// Gets all pages under the specified root.
    /// </summary>
    /// <param name="rootLink">The root page.</param>
    /// <returns>All pages under the specified root (including descendant pages).</returns>
    public IEnumerable<PageData> GetAll<T>(ContentReference rootLink)
        where T : PageData
    {
        var children = _contentLoader.GetChildren<T>(rootLink);
        foreach (var child in children)
        {
            if (child is T childOfRequestedType)
            {
                yield return childOfRequestedType;
            }

            foreach (var descendant in GetAll<T>(child.ContentLink))
            {
                yield return descendant;
            }
        }
    }

    /// <summary>
    /// Gets all pages under the specified root.
    /// </summary>
    /// <param name="rootLink">The root page.</param>
    /// <param name="includeDescendant">True if show the descendant pages, False don't show</param>
    /// <param name="pageTypeId">The page type ID to filter by.</param>
    public IEnumerable<PageData> FindPagesByPageType(PageReference pageLink, bool includeDescendant, int pageTypeId)
    {
        if (ContentReference.IsNullOrEmpty(pageLink))
        {
            throw new ArgumentNullException(nameof(pageLink), "No page link provided, unable to find pages");
        }

        return includeDescendant
            ? FindPagesByPageTypeRecursively(pageLink, pageTypeId)
            : _contentLoader.GetChildren<PageData>(pageLink);
    }

    /// <summary>
    /// Gets all pages under the specified root (including descendant pages).
    /// </summary>
    /// <param name="rootLink">The root page.</param>
    /// <param name="pageTypeId">The page type ID to filter by.</param>
    private IEnumerable<PageData> FindPagesByPageTypeRecursively(PageReference pageLink, int pageTypeId)
    {
        var criteria = new PropertyCriteriaCollection
        {
            new PropertyCriteria
            {
                Name = "PageTypeID",
                Condition = CompareCondition.Equal,
                Type = PropertyDataType.PageType,
                Value = pageTypeId.ToString(CultureInfo.InvariantCulture)
            }
        };

        if (_contentProviderManager.ProviderMap.CustomProvidersExist)
        {
            var contentProvider = _contentProviderManager.ProviderMap.GetProvider(pageLink);
            if (contentProvider.HasCapability(ContentProviderCapabilities.Search))
            {
                criteria.Add(new PropertyCriteria
                {
                    Name = "EPI:MultipleSearch",
                    Value = contentProvider.ProviderKey
                });
            }
        }

        return _pageCriteriaQueryService.FindPagesWithCriteria(pageLink, criteria);
    }
}