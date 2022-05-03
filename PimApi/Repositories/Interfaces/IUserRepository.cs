using PimModels.Models;

namespace PimApi.Repositories.Interfaces;

public interface IUserRepository
{
    Task CreateAsync(User user);
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByUsernameAsync(string username);
    Task<User?> GetUserByIdAsync(int userId);
}
