using System.ComponentModel.DataAnnotations;
using EPiServer.Filters;
using EPiServer.Framework.Localization;
using EPiServer.ServiceLocation;
using realloy.Extensions;
using realloy.Models.Blocks;

namespace realloy.Models.Pages;

[SiteContentType(GUID = "2065f18a-96b7-4fc5-8bb4-d326b56ac3f8")]
public class NewsPage: StandardPage
{
    [Display(
        Name = "News List",
        GroupName = SystemTabNames.Content,
        Order = 310)]
    public virtual PageListBlock NewsList { get; set; }

    public override void SetDefaultValues(ContentType contentType)
    {
        base.SetDefaultValues(contentType);
        NewsList.Count = 20;
        NewsList.Heading = ServiceLocator.Current.GetInstance<LocalizationService>().GetString("/newspagetemplate/latestnews");
        NewsList.IncludeIntroduction = true;
        NewsList.IncludePublishDate = true;
        NewsList.Recursive = true;
        NewsList.PageTypeFilter = typeof(ArticlePage).GetPageType();
        NewsList.SortOrder = FilterSortOrder.PublishedDescending;
    }
}