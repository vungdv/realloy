using EPiServer.SpecializedProperties;
using Microsoft.AspNetCore.Html;
using realloy.Models.Blocks;

namespace realloy.Models.ViewModels;

public class LayoutModel
{
    public SiteLogoTypeBlock LogoType { get; set; }
    public IHtmlContent LogoTypeLinkUrl { get; set; }
    public LinkItemCollection ProductPages {get;set;} = null!;
    public LinkItemCollection CompanyInformationPages {get;set;} = null!;
    public LinkItemCollection NewsPages {get;set;} = null!;
    public LinkItemCollection CustomerZonePages {get;set;} = null!;
    public bool HideHeader { get; internal set; }
    public bool HideFooter { get; internal set; }
    public bool IsInReadonlyMode { get; set; }
}