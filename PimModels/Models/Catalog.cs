using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PimModels.Models;

public class Catalog
{
    [Key, Column(Order = 1)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }

    //public int DefualtCurrencyCode { get; set; }
    public Currency DefaultCurrency { get; set; }

    public List<CatalogUser> CatalogUsers { get; set; }
}
