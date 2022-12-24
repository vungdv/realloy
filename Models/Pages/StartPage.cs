namespace realloy.Models.Pages;

/// <summary>
/// Used for the site's start page and also acts as a container for site settings
/// </summary>
[ContentType(
    GUID = "19671657-B684-4D95-A61F-8DD4FE60D559",
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
public class StartPage: SitePageData
{

}