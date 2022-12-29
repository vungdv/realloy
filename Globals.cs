using System.ComponentModel.DataAnnotations;

namespace realloy;

public static class Globals
{
    // [GroupDefinitions]
    public static class GroupNames
    {
        [Display(Name = "Default", Order = 2)]
        public const string Default = "Default";
        public const string Products = "Products";
        public const string SiteSettings = "SiteSettings";
        [Display(Name = "Metadata", Order = 3)]
        public const string MetaData = "Metadata";
        [Display(Name = "Specialized", Order = 7)]
        public const string Specialized = "Specialized";
    }

    /// <summary>
    /// Tags to use for the main widths used in the Bootstrap HTML framework
    /// </summary>
    public static class ContentAreaTags
    {
        public const string FullWidth = "full";
        public const string WideWidth = "wide";
        public const string HalfWidth = "half";
        public const string NarrowWidth = "narrow";
        public const string NoRenderer = "norenderer";
    }

    /// <summary>
    /// Virtual path to folder with static graphics, such as "/gfx"
    /// </summary>
    public const string StaticGraphicsFolderPath = "/gfx/";
}