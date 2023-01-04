using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using realloy.Models.Blocks;

namespace realloy.Components;

public class PageListBlockComponent : BlockComponent<PageListBlock>
{
    protected override IViewComponentResult InvokeComponent(PageListBlock currentContent)
    {
        return View(currentContent);
    }
}