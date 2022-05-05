using Microsoft.EntityFrameworkCore;
using PimApi.Data;
using PimApi.Repositories.Interfaces;
using PimModels.Models;

namespace PimApi.Repositories;

public class CurrencyRepository : ICurrencyRepository
{
    private readonly PimDbContext _context;
    public CurrencyRepository(PimDbContext context)
    {
        _context = context;
    }

    public async Task<List<Currency>> GetAllAsync() => await _context.Currencies.ToListAsync();
}
