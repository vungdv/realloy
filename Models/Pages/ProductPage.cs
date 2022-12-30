using System.ComponentModel.DataAnnotations;
using realloy.Models.Blocks;

namespace realloy.Models.Pages;

[SiteContentType(
    GUID = "dc15165f-1d9d-40e8-98ae-3c5dc656a75f",
    GroupName = Globals.GroupNames.Products)]
public class ProductPage : StandardPage, IHasRelatedContent
{
    [Display(
        Name = "Related Content Area",
        GroupName = SystemTabNames.Content,
        Order = 310)]
    [AllowedTypes(
        allowedTypes: new[] { typeof(IContentData) },
        restrictedTypes: new[] { typeof(JumbotronBlock) })]
    public virtual ContentArea RelatedContentArea { get; set; }

    [Required]
    [Display(Order = 305)]
    [UIHint(Globals.SiteUIHints.StringsCollection)]
    [CultureSpecific]
    public virtual IList<string> UniqueSellingPoints { get; set; }
}