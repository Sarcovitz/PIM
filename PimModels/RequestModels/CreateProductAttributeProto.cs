using PimModels.Models;
using System.ComponentModel.DataAnnotations;

namespace PimModels.RequestModels;

public class CreateProductAttributeProto
{
    [Required]
    [RegularExpression(@"[A-Za-z0-9\s]{3,20}", ErrorMessage = "Catalog name can contain only letters and numbers.")]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "Catalog name length must be between 3 and 20 characters long.")]
    public string? Name { get; set; }
    [Required]
    public AttributeTypeEnum AttributeType { get; set; }
    [Required]
    public string? DefaultValue { get; set; } = null;
    public string? PossibleValues { get; set; } = null;

    [Required]
    public int CatalogId { get; set; }

    List<Category> Categories { get; set; } = new();
}
