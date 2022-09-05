using PimModels.Models;

namespace PimModels.RequestModels;

public class CreateProduct
{
    public string? Name { get; set; }
    public string? Sku { get; set; }
    public string? Ean { get; set; }
    public string? Description { get; set; }
    public string? DescriptionHTML { get; set; }
    public double Height { get; set; }
    public double Width { get; set; }
    public double Length { get; set; }
    public int CatalogId { get; set; }
    public int? CategoryId { get; set; }
    virtual public List<ProductAttribute> ProductAttributes { get; set; }
    virtual public List<CreateProductImage> ProductImages { get; set; }
}
