using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PimModels.Models;

public  class Category
{
    [Key, Column(Order = 1)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }

    public int CatalogId { get; set; }
    virtual public Catalog Catalog { get; set; }

    public int? ParentCategoryId { get; set; } = null;
    virtual public Category? ParentCategory { get; set; }

    virtual public List<Category>? SubCategories { get; set; }

    virtual public List<Product>? Products { get; set; }

    virtual public List<CategoryProductAttributeProto>? AttributeProtos { get; set; }
}
