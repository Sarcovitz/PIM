using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PimModels.Models;

public class Product
{
    [Key, Column(Order = 1)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Sku { get; set; }
    public string? Ean { get; set; }
    public string? Description { get; set; }
    public string? DescriptionHTML { get; set; }
    public double? Height { get; set; }
    public double Width { get; set; }
    public double Length { get; set; }

    public int CatalogId { get; set; }
    virtual public Catalog? Catalog { get; set; }

    public int? CategoryId { get; set; }
    virtual public Category? Category { get; set; }

    virtual public List<ProductAttribute> ProductAttributes { get; set; }
    virtual public List<ProductImage> ProductImages { get; set; }
}
