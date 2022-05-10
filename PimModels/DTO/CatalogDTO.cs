using PimModels.Models;

namespace PimModels.DTO;

public class CatalogDTO
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string DefaultCurrencyCode { get; set; }
    virtual public List<CatalogUser> CatalogUsers { get; set; }
}
