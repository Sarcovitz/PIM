using PimApi.Data;
using PimApi.Repositories.Interfaces;
using PimModels.Models;
using Microsoft.EntityFrameworkCore;

namespace PimApi.Repositories;

public class UserRepository : IUserRepository
{
    private readonly PimDbContext _context;
    public UserRepository(PimDbContext context) => _context = context;

    public async Task CreateAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetUserByEmailAsync(string email) => await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

    public async Task<User?> GetUserByIdAsync(int userId) => await _context.Users.FirstOrDefaultAsync(x=>x.Id == userId);

    public async Task<User?> GetUserByUsernameAsync(string username) => await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
}
