namespace realloy.Models;

public class SiteContentTypeAttribute : ContentTypeAttribute
{
    public SiteContentTypeAttribute()
    {
        GroupName = Globals.GroupNames.Default;
    }
}