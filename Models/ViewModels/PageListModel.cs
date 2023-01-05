namespace realloy.Models.ViewModels;

public class PageListModel
{
    public PageListModel(IEnumerable<PageData> pages, bool showIntroduction, bool showPublishDate)
    {
        Pages = pages;
        ShowIntroduction = showIntroduction;
        ShowPublishDate = showPublishDate;
    }
    public string Heading { get; set; }
    public IEnumerable<PageData> Pages { get; set; }
    public bool ShowIntroduction { get; set; }
    public bool ShowPublishDate { get; set; }
}