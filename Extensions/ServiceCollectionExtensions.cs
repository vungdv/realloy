using Microsoft.AspNetCore.Mvc;
using realloy.Business.UIDescriptors;

namespace realloy.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddReAlloy(this IServiceCollection services)
    {

        services.Configure<MvcOptions>(options => options.Filters.Add<PageContextActionFilter>());
        return services;
    }
}