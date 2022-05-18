using PimModels.Models;

namespace PimApi.Services.Interfaces;

public interface IUserService
{
    Task<User?>GetUserByNameAsync(string username);
    Task<User?> GetUserByIdAsync(int id);
}
