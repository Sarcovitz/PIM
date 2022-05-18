using PimApi.Repositories.Interfaces;
using PimApi.Services.Interfaces;
using PimModels.Models;

namespace PimApi.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> GetUserByIdAsync(int id) => await _userRepository.GetUserByIdAsync(id);

    public async Task<User?> GetUserByNameAsync(string username) => await _userRepository.GetUserByUsernameAsync(username);
}
