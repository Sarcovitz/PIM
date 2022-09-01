using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PimModels.Models;

public  class ProductImage
{
    [Key, Column(Order = 1)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public byte[] Image { get; set; }

    public int ProductId { get; set; }
    public virtual Product Product { get; set; }

}
