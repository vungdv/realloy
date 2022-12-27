using EPiServer.Web;
using EPiServer.Web.Mvc.Html;
using Microsoft.AspNetCore.Mvc;
using realloy.Business.Rendering;
using realloy.Business.UIDescriptors;

namespace realloy.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddReAlloy(this IServiceCollection services)
    {
        services.Configure<MvcOptions>(options => options.Filters.Add<PageContextActionFilter>());
         services.AddTransient<ContentAreaRenderer, ReAlloyContentAreaRender>();
        services.Configure<DisplayOptions>(options =>
        {
            options
            .Add("full", "/displayoptions/full", Globals.ContentAreaTags.FullWidth, string.Empty, "epi-icon__layout--full")
            .Add("wide", "/displayoptions/wide", Globals.ContentAreaTags.WideWidth, string.Empty, "epi-icon__layout--wide")
            .Add("half", "/displayoptions/half", Globals.ContentAreaTags.HalfWidth, string.Empty, "epi-icon__layout--half")
            .Add("narrow", "/displayoptions/narrow", Globals.ContentAreaTags.NarrowWidth, string.Empty, "epi-icon__layout--narrow");
        });

        return services;
    }
}