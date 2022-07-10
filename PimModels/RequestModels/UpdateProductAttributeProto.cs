using PimModels.Models;

namespace PimModels.RequestModels;

public class UpdateProductAttributeProto
{
    public string? Name { get; set; }
    public AttributeTypeEnum AttributeType { get; set; }
    public string? DefaultValue { get; set; } = null;
    public string? PossibleValues { get; set; } = null;
}
