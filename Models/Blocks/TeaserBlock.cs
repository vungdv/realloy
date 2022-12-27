using System.ComponentModel.DataAnnotations;
using EPiServer.Web;

namespace realloy.Models.Blocks;

[SiteContentType(GUID = "d856550d-05b7-4180-9ffa-bb698d95d7da")]
[SiteImageUrl]
public class TeaserBlock : SiteBlockData
{
    [CultureSpecific]
    [Display(
        Name = "Heading",
        Description = "Heading for the teaser",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [Required(AllowEmptyStrings = false)]
    public virtual string Heading { get; set; }

    [CultureSpecific]
    [Display(
        Name = "Description",
        Description = "Description for the teaser",
        GroupName = SystemTabNames.Content,
        Order = 20)]
    [Required(AllowEmptyStrings = false)]
    [UIHint(UIHint.Textarea)]
    public virtual string Description { get; set; }

    [Display(
        GroupName = SystemTabNames.Content,
        Order = 4)]
    public virtual PageReference Link { get; set; }

    [CultureSpecific]
    [Display(
        Name = "Image",
        Description = "Image for the teaser",
        GroupName = SystemTabNames.Content,
        Order = 40)]
    [Required(AllowEmptyStrings = false)]
    [UIHint(UIHint.Image)]
    public virtual ContentReference Image { get; set; }
}