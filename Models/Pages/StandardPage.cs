using System.ComponentModel.DataAnnotations;

namespace realloy.Models.Pages;

[ContentType(
    DisplayName = "Standard page",
    Description = "Use this page type for standard pages.",
    AvailableInEditMode = true,
    GroupName = "Default",
    GUID = "0a235c44-aeb1-41eb-894a-4c754603efe6")]
[ImageUrl(Globals.StaticGraphicsFolderPath + "page-type-thumbnail-standard.png")]
public class StandardPage : SitePageData
{
    [Display(
        GroupName = SystemTabNames.Content,
        Order = 310)]
    public virtual XhtmlString MainBody { get; set; } = null!;

    [Display(
        GroupName = SystemTabNames.Content,
        Order = 320)]
    public virtual ContentArea MainContentArea { get; set; } = null!;
}
