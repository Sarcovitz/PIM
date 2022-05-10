using System.ComponentModel.DataAnnotations.Schema;

namespace PimModels.Models;

public class CatalogUser
{
    public int CatalogId { get; set; }
    virtual public Catalog Catalog { get; set; }

    public int UserId { get; set; }
    virtual public User User { get; set; }

    public CatalogUserRole Role { get; set; }
}

public enum CatalogUserRole
{
    Owner = 1,
    Worker = 2,
    Manager = 3,
    Viewer = 4,
}
