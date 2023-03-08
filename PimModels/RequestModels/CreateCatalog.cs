using System.ComponentModel.DataAnnotations;

namespace PimModels.RequestModels;

public class CreateCatalog
{
    [Required]
    [RegularExpression(@"[A-Za-z0-9\s]{3,20}", ErrorMessage = "Catalog name can contain only letters and numbers.")]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "Catalog name length must be between 3 and 20 characters long.")]
    public string Name { get; set; }
    [Required]
    public string DefaultCurrencyCode { get; set; }
}
