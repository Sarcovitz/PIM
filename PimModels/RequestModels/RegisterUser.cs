using System.ComponentModel.DataAnnotations;

namespace PimModels.RequestModels;

public class RegisterUser
{
 
    public string Username { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Enter valid E-mail address.")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "Password length must be between 3 and 20 characters long.")]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Both passwords must be the same.")]
    public string Password2 { get; set; }
}
