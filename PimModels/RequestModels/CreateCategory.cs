using PimModels.Models;

namespace PimModels.RequestModels;

public class CreateCategory
{
    public string Name { get; set; }
    public int CatalogId { get; set; }
    public int? ParentCategoryId { get; set; } = null;
    public List<ProductAttributeProto>? AttributeProtos { get; set; }
}
