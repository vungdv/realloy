using System.ComponentModel.DataAnnotations;
using EPiServer.Web;

namespace realloy.Models.Blocks;

[SiteContentType(
    GUID = "ce365189-e565-46fe-b706-c3ed8021bc95",
    GroupName = Globals.GroupNames.Specialized)]
public class JumbotronBlock : SiteBlockData
{
    [Display(
        GroupName = SystemTabNames.Content,
        Order = 1)]
    [CultureSpecific]
    [UIHint(UIHint.Image)]
    public virtual ContentReference Image { get; set; }
    [Display(
        GroupName = SystemTabNames.Content,
        Order = 2)]
    [CultureSpecific]
    [UIHint(UIHint.Textarea)]
    public virtual string ImageDescription
    {
        get
        {
            var propertyValue = this.GetPropertyValue(x => x.ImageDescription);
            return string.IsNullOrWhiteSpace(propertyValue) ? Heading : propertyValue;
        }
        set
        {
            this.SetPropertyValue(x => x.ImageDescription, value);
        }
    }
    [Display(
        GroupName = SystemTabNames.Content,
        Order = 3)]
    [CultureSpecific]
    public virtual string Heading { get; set; }
    [Display(
        GroupName = SystemTabNames.Content,
        Order = 4)]
    [CultureSpecific]
    public virtual string SubHeading { get; set; }
    [Display(
        GroupName = SystemTabNames.Content,
        Order = 5)]
    [CultureSpecific]
    [Required]
    public virtual string ButtonText { get; set; }
    [Display(
        GroupName = SystemTabNames.Content,
        Order = 6)]
    [CultureSpecific]
    [Required]
    public virtual Url ButtonLink { get; set; }
}