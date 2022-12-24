namespace realloy.Models;

public class SiteImageUrl : ImageUrlAttribute
{
    public SiteImageUrl()
        : base("/gfx/page-type-thumbnail.png")
    {
    }

    public SiteImageUrl(string path) 
        : base(path)
    {
    }
}