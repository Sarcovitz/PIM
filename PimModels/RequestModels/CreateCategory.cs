using PimModels.Models;
using System.ComponentModel.DataAnnotations;

namespace PimModels.RequestModels;

public class CreateCategory
{
    [Required]
    [RegularExpression(@"[A-Za-z0-9\s]{3,20}", ErrorMessage = "Category name can contain only letters and numbers.")]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "Category name length must be between 3 and 20 characters long.")]
    public string Name { get; set; }
    public int CatalogId { get; set; }
    public int? ParentCategoryId { get; set; } = null;
    public List<ProductAttributeProto>? AttributeProtos { get; set; }
}
