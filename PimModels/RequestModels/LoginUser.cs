using System.ComponentModel.DataAnnotations;

namespace PimModels.RequestModels;

public class LoginUser
{
    [Required]
    public string? Username { get; set; }
    [Required]
    public string? Password { get; set; }
}
