using EPiServer.Web;
using realloy.Business.Rendering;
using System.ComponentModel.DataAnnotations;

namespace realloy.Models.Pages;

public abstract class SitePageData : PageData, ICustomCssInContentArea
{
    [Display(
        GroupName = Globals.GroupNames.MetaData,
        Order = 100)
    ]
    [CultureSpecific]
    public virtual string MetaTitle
    {
        get
        {
            var metaTitle = this.GetPropertyValue(p => p.MetaTitle);
            return !string.IsNullOrWhiteSpace(metaTitle)
                    ? metaTitle
                    : PageName;
        }
        set => this.SetPropertyValue(p => p.MetaTitle, value);
    }
    [Display(
        GroupName = Globals.GroupNames.MetaData,
        Order = 200)]

    [CultureSpecific]
    public virtual IList<string> MetaKeywords { get; set; } = null!;

    [Display(
        GroupName = Globals.GroupNames.MetaData,
        Order = 300)]
    [CultureSpecific]
    public virtual string MetaDescription { get; set; } = null!;

    [Display(
        GroupName = Globals.GroupNames.MetaData,
        Order = 400)]
    public virtual bool DisableIndexing { get; set; }
    [Display(
        GroupName = SystemTabNames.Content,
        Order = 100)]
    [UIHint(UIHint.Image)]
    public virtual ContentReference PageImage { get; set; } = null!;

    [Display(
        GroupName = SystemTabNames.Content,
        Order = 200)]
    [UIHint(UIHint.Textarea)]
    [CultureSpecific]
    public virtual string TeaserText
    {
        get
        {
            var teaserText = this.GetPropertyValue(p => p.TeaserText);
            return !string.IsNullOrWhiteSpace(teaserText)
                ? teaserText
                : MetaDescription;
        }
        set => this.SetPropertyValue(p => p.TeaserText, value);
    }

    [Display(
        GroupName = SystemTabNames.Settings,
        Order = 200)]
    [CultureSpecific]
    public virtual bool HideSiteHeader { get; set; }

    [Display(
        GroupName = SystemTabNames.Settings,
        Order = 300)]
    [CultureSpecific]
    public virtual bool HideSiteFooter { get; set; }

    public string ContentAreaCssClass => "teaserblock";
}