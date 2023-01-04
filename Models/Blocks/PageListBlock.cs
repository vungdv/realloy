using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EPiServer.Filters;

namespace realloy.Models.Blocks;

[SiteContentType(GUID = "ad46aa6b-1d06-4752-82f0-644e0e484cc4")]
[SiteImageUrl]
public class PageListBlock : SiteBlockData
{
    [CultureSpecific]
    [Display(
        Name = "Heading",
        Description = "The heading of the block",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    public virtual string Heading { get; set; }

    [DefaultValue(false)]
    [Display(
        GroupName = SystemTabNames.Content,
        Order = 20)]
    public virtual bool IncludePublishDate { get; set; }
    [DefaultValue(false)]
    [Display(
        GroupName = SystemTabNames.Content,
        Order = 30)]
    public virtual bool IncludeIntroduction { get; set; }
    [Required]
    [DefaultValue(3)]
    [Display(
        GroupName = SystemTabNames.Content,
        Order = 40)]
    public virtual int Count { get; set; }
    [DefaultValue(FilterSortOrder.PublishedDescending)]
    [Display(
        GroupName = SystemTabNames.Content,
        Order = 50)]
    // [UIHint("SortOrder")]
    [BackingType(typeof(PropertyNumber))]
    public virtual FilterSortOrder SortOrder { get; set; }
    [Display(
        GroupName = SystemTabNames.Content,
        Order = 60)]
    [Required]
    public virtual PageReference Root { get; set; }

    [Display(
        GroupName = SystemTabNames.Content,
        Order = 70)]
    public virtual PageType PageTypeFilter { get; set; }

    [Display(
        GroupName = SystemTabNames.Content,
        Order = 80)]
    public virtual CategoryList CategoryFilter { get; set; }
    [Display(
        GroupName = SystemTabNames.Content,
        Order = 90)]
    public virtual bool Recursive { get; set; }
    public override void SetDefaultValues(ContentType contentType)
    {
        base.SetDefaultValues(contentType);
        Count = 3;
        SortOrder = FilterSortOrder.PublishedDescending;
        IncludeIntroduction = true;
        IncludePublishDate = false;
    }
}