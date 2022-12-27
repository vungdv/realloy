using EPiServer.Web.Mvc.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using static realloy.Globals;

namespace realloy.Business.Rendering;

public class ReAlloyContentAreaRender : ContentAreaRenderer
{
    protected override string GetContentAreaItemCssClass(
        IHtmlHelper htmlHelper,
        ContentAreaItem contentAreaItem)
    {
        var baseItemCssClass = base.GetContentAreaItemCssClass(htmlHelper, contentAreaItem);
        var baseTag = base.GetContentAreaItemHtmlTag(htmlHelper, contentAreaItem);
        return $"block {baseItemCssClass} {GetTypeSpecificCssClasses(contentAreaItem)} {GetCssClassForTag(baseTag)}";
    }
    /// <summary>
    /// Gets a CSS class used for styling based on a tag name (ie a Bootstrap class name)
    /// </summary>
    /// <param name="tagName">Any tag name available, see <see cref="ContentAreaTags"/></param>
    private static string GetCssClassForTag(string tagName)
    {
        if (string.IsNullOrEmpty(tagName))
        {
            return string.Empty;
        }

        return tagName.ToLowerInvariant() switch
        {
            ContentAreaTags.FullWidth => "col-12",
            ContentAreaTags.WideWidth => "col-12 col-md-8",
            ContentAreaTags.HalfWidth => "col-12 col-sm-6",
            ContentAreaTags.NarrowWidth => "col-12 col-sm-6 col-md-4",
            _ => string.Empty,
        };
    }
    /// <summary>
    /// Gets the CSS class for the content area item: originaltype, custom css class
    /// </summary>
    private static string GetTypeSpecificCssClasses(ContentAreaItem contentAreaItem)
    {
        var content = contentAreaItem.GetContent();
        var cssClass = content == null ? string.Empty : content.GetOriginalType().Name.ToLowerInvariant();

        if (content is ICustomCssInContentArea customClassContent &&
            !string.IsNullOrWhiteSpace(customClassContent.ContentAreaCssClass))
        {
            cssClass += $" {customClassContent.ContentAreaCssClass}";
        }

        return cssClass;
    }
}