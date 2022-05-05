using PimModels.Models;

namespace PimApi.Repositories.Interfaces;

public interface ICurrencyRepository
{
    Task<List<Currency>> GetAllAsync();
}
