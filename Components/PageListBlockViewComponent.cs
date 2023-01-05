using EPiServer.Filters;
using EPiServer.Web;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using realloy.Business;
using realloy.Models.Blocks;
using realloy.Models.ViewModels;

namespace realloy.Components;

public class PageListBlockViewComponent : BlockComponent<PageListBlock>
{
    private readonly IContentLoader _contentLoader;
    private readonly ContentLocator _contentLocator;

    public PageListBlockViewComponent(
        IContentLoader contentLoader,
        ContentLocator contentLocator)
    {
        _contentLoader = contentLoader;
        _contentLocator = contentLocator;
    }

    protected override IViewComponentResult InvokeComponent(PageListBlock currentContent)
    {
        var pages = FindPages(currentContent);

        pages = Sort(pages, currentContent.SortOrder);

        if(currentContent.Count > 0)
        {
            pages = pages.Take(currentContent.Count);
        }

        var model = new PageListModel(
            pages,
            currentContent.IncludeIntroduction,
            currentContent.IncludePublishDate)
        {
            Heading = currentContent.Heading
        };

        ViewData.GetEditHints<PageListModel, PageListBlock>()
            .AddConnection(m => m.Heading, c => c.Heading)
            .AddConnection(m => m.ShowIntroduction, c => c.IncludeIntroduction)
            .AddConnection(m => m.ShowPublishDate, c => c.IncludePublishDate);

        return View(model);
    }

    private IEnumerable<PageData> FindPages(PageListBlock currentBlock)
    {
        IEnumerable<PageData> pages;
        var listRoot = currentBlock.Root;

        if (currentBlock.Recursive)
        {
            if (currentBlock.PageTypeFilter != null)
            {
                pages = _contentLocator.FindPagesByPageType(listRoot, true, currentBlock.PageTypeFilter.ID);
            }
            else
            {
                pages = _contentLocator.GetAll<PageData>(listRoot);
            }
        }
        else
        {
            if (currentBlock.PageTypeFilter != null)
            {
                pages = _contentLoader
                        .GetChildren<PageData>(listRoot)
                        .Where(p => p.ContentTypeID == currentBlock.PageTypeFilter.ID);
            }
            else
            {
                pages = _contentLoader.GetChildren<PageData>(listRoot);
            }
        }

        if (currentBlock.CategoryFilter?.Any() == true)
        {
            pages = pages.Where(p => p.Category.Intersect(currentBlock.CategoryFilter).Any());
        }

        return pages;
    }

    private static IEnumerable<PageData> Sort(IEnumerable<PageData> pages, FilterSortOrder sortOrder)
    {
        var sortFilter = new FilterSort(sortOrder);
        sortFilter.Sort(new PageDataCollection(pages.ToList()));
        return pages;
    }
}