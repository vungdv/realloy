using EPiServer.SpecializedProperties;

namespace realloy.Models.ViewModels;

public class LayoutModel
{
    public LinkItemCollection ProductPages {get;set;} = null!;
    public LinkItemCollection CompanyInformationPages {get;set;} = null!;
    public LinkItemCollection NewsPages {get;set;} = null!;
    public LinkItemCollection CustomerZonePages {get;set;} = null!;
    public bool HideHeader { get; internal set; }
    public bool HideFooter { get; internal set; }
    public bool IsInReadonlyMode { get; set; }
}