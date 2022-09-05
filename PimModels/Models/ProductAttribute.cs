using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PimModels.Models;

public class ProductAttribute
{
    [Key, Column(Order = 1)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Value { get; set; }

    public int AttributeProtoId { get; set; }
    public ProductAttributeProto? AttributeProto { get; set; }

    public int ProductId { get; set; }
    public Product? Product { get; set; }
}
