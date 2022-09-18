using PimModels.Models;
using System.ComponentModel.DataAnnotations;

namespace PimModels.RequestModels;

public class UpdateProduct
{
    [Required]
    [RegularExpression(@"[A-Za-z0-9]{3,20}", ErrorMessage = "Product name can contain only letters and numbers.")]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "Product name length must be between 3 and 20 characters long.")]
    public string? Name { get; set; }
    [Required]
    public string? Sku { get; set; }
    [Required]
    public string? Ean { get; set; }
    public string? Description { get; set; }
    public string? DescriptionHTML { get; set; }
    public double Height { get; set; }
    public double Width { get; set; }
    public double Length { get; set; }
    public int CatalogId { get; set; }
    public int? CategoryId { get; set; }
    virtual public List<ProductAttribute> ProductAttributes { get; set; }
    virtual public List<UpdateProductImage> ProductImages { get; set; }
}
