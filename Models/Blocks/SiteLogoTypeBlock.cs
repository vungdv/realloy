using System.ComponentModel.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;

namespace realloy.Models.Blocks;

[SiteContentType(GUID = "0511a013-0ab7-47a8-a5b9-5313a3cf9914")]
public class SiteLogoTypeBlock : SiteBlockData
{
    /// <summary>
    /// Gets the site logotype URL
    /// </summary>
    /// <remarks>If not specified a default logotype will be used</remarks>
    // [DefaultDragAndDropTarget]
    [UIHint(UIHint.Image)]
    public virtual Url Url
    {
        get
        {
            var url = this.GetPropertyValue(x => x.Url);
            return url?.IsEmpty() != false ?
             new Url("/gfx/logotype.png")
             : url;
        }
        set => this.SetPropertyValue(x => x.Url, value);
    }
    [CultureSpecific]
    public virtual string Title { get; set; }
}