using EPiServer.ServiceLocation;

namespace realloy.Helpers;

public static class CategorizableExtensions
{
    public static string[] GetThemeCssClassNames(this ICategorizable content)
    {
        if (content == null)
        {
            return Array.Empty<string>();
        }

        var cssClasses = new HashSet<string>();
        var categoryRepository = ServiceLocator.Current.GetRequiredService<CategoryRepository>();

        foreach (var categoryName in content.Category.Select(category => categoryRepository.Get(category).Name))
        {
            switch (categoryName.ToLowerInvariant())
            {
                case "meet":
                    cssClasses.Add("theme1");
                    break;
                case "track":
                    cssClasses.Add("theme2");
                    break;
                case "plan":
                    cssClasses.Add("theme3");
                    break;
                default:
                    break;
            }
        }
        return cssClasses.ToArray();
    }
}