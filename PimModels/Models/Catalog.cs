using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PimModels.Models;

public class Catalog
{
    [Key, Column(Order = 1)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }

    public string DefaultCurrencyCode { get; set; }
    virtual public Currency DefaultCurrency { get; set; }

    virtual public List<CatalogUser> CatalogUsers { get; set; }
}
