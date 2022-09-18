using PimModels.Models;

namespace PimModels.RequestModels;

public class UpdateProductAttribute
{
    public int Id { get; set; }
    public bool boolVal
    {
        get => bool.TryParse(Value, out bool boolVal) ? boolVal : false;
        set => Value = value.ToString();
    }
    public int intVal
    {
        get => int.TryParse(Value, out int intVal) ? intVal : 0;
        set => Value = value.ToString();
    }
    public float floatVal
    {
        get => float.TryParse(Value, out float floatVal) ? floatVal : 0;
        set => Value = value.ToString();
    }
    public string Value { get; set; }

    public int AttributeProtoId { get; set; }
    public ProductAttributeProto? AttributeProto { get; set; }
}
