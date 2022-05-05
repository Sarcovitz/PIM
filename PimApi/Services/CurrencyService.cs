using PimApi.Repositories.Interfaces;
using PimApi.Services.Interfaces;
using PimModels.Models;

namespace PimApi.Services;

public class CurrencyService : ICurrencyService
{
    private readonly ICurrencyRepository _currencyRepository;

    public CurrencyService(ICurrencyRepository currencyRepository)
    {
        _currencyRepository = currencyRepository;
    }

    public async Task<List<Currency>> GetAllAsync() => await _currencyRepository.GetAllAsync();
}
