using PimModels.DTO;
using PimModels.Models;
using PimModels.RequestModels;

namespace PimApi.Services.Interfaces;

public interface IAuthenticationService
{
    Task<LoginUserDTO?> LoginAsync(LoginUser loginUser);
    Task RegisterAsync(RegisterUser registerUser);
    Task<User?> GetCurrentUserAsync(int userId);
}
