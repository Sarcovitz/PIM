using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PimModels.Models;

public class Currency
{
    [Key, Column(Order = 1)]
    public int Code { get; set; }
    public string Fullname { get; set; }
}
