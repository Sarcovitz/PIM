using PimModels.Models;

namespace PimModels.DTO;

public class ProductAttributeProtoDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public AttributeTypeEnum AttributeType { get; set; }
    public string? DefaultValue { get; set; } = null;
    public string? PossibleValues { get; set; } = null;

    virtual public List<CategoryProductAttributeProto>? Categories { get; set; }
}
