using System.ComponentModel.DataAnnotations;
using EPiServer.SpecializedProperties;
using realloy.Models.Blocks;

namespace realloy.Models.Pages;

/// <summary>
/// Used for the site's start page and also acts as a container for site settings
/// </summary>
[ContentType(
    GUID = "8ddc526d-2999-42f6-a6ef-3dbf49506ec5",
    GroupName = Globals.GroupNames.Specialized)]
[SiteImageUrl]
[AvailableContentTypes(
    Availability.Specific,
    Include = new[]
    {
        typeof(StandardPage),
        typeof(ContentFolder) }, // Pages we can create under the start page...
    ExcludeOn = new[]
    {
        typeof(StandardPage)
    })] // ...and underneath those we can't create additional start pages
public class StartPage : SitePageData
{
    [Display(
        GroupName = SystemTabNames.Content,
        Order = 320)]
    [CultureSpecific]
    public virtual ContentArea MainContentArea { get; set; }
    [Display(GroupName = Globals.GroupNames.SiteSettings, Order = 330)]
    public virtual LinkItemCollection ProductPageLinks { get; set; }
    [Display(GroupName = Globals.GroupNames.SiteSettings, Order = 340)]
    public virtual LinkItemCollection CompanyInformationPageLinks { get; set; }
    [Display(GroupName = Globals.GroupNames.SiteSettings, Order = 350)]
    public virtual LinkItemCollection NewsPageLinks { get; set; }
    [Display(GroupName = Globals.GroupNames.SiteSettings, Order = 350)]
    public virtual LinkItemCollection CustomerZonePageLinks { get; set; }
    public virtual SiteLogoTypeBlock SiteLogoType { get; set; }
}