using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PimModels.Models;

public class ProductAttributeProto
{
    [Key, Column(Order = 1)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? Name { get; set; }
    public AttributeTypeEnum AttributeType { get; set; }
    public string? DefaultValue { get; set; } = null;
    public string? PossibleValues { get; set; } = null;

    public int CatalogId { get; set; }
    virtual public Catalog Catalog { get; set; }
    virtual public List<ProductAttribute> ProductAttributes { get; set; }
    virtual public List<Category>? Categories { get; set; }

}

public enum AttributeTypeEnum
{
    Integer = 1,
    Float = 2,
    String = 3,
    List = 4,
    Bool = 5,
}
