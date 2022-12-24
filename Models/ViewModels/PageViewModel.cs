using realloy.Models.Pages;

namespace realloy.Models.ViewModels;

public class PageViewModel<T> : IPageViewModel<T> where T : SitePageData
{
    public PageViewModel(T currentPage)
    {
        CurrentPage = currentPage;
    }

    public T CurrentPage { get; }

    public LayoutModel Layout { get; set; }
    public IContent Section { get; set;}
}