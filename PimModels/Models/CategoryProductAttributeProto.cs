namespace PimModels.Models;

public class CategoryProductAttributeProto
{
    public int CategoryId { get; set; }
    virtual public Category? Category { get; set; }

    public int ProductAttributeProtoId { get; set; }
    virtual public ProductAttributeProto? ProductAttributeProto { get; set; }
}
