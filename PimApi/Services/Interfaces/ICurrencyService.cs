using PimModels.Models;

namespace PimApi.Services.Interfaces;

public interface ICurrencyService
{
    Task<List<Currency>> GetAllAsync();
}
