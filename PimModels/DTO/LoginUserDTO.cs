namespace PimModels.DTO;

public class LoginUserDTO
{
    public string Token { get; set; }
    public long ExpirationDate { get; set; }
    public int UserId { get; set; }
}
