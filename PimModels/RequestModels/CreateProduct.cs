using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PimModels.RequestModels;

public class CreateProduct
{
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Sku { get; set; }
    public string? Ean { get; set; }
    [Required]
    public int CatalogId { get; set; }
    public int CategoryId { get; set; }
}
