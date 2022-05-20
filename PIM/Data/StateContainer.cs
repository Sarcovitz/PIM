using PimModels.Models;

namespace PIM.Data;

public class StateContainer
{
    public event Action? OnChange;

    public User? User { get; set; } = null;
    public Catalog? Catalog { get; set; } = null;

    private void NotifyStateChanged() => OnChange?.Invoke();

    public async Task SetUser(User user)
    {
        User = user;
        NotifyStateChanged();
    }

    public async Task SetCatalog(Catalog catalog)
    {
        Catalog = catalog;
        NotifyStateChanged();
    }

    public async Task Reset()
    {
        User = null;
        Catalog = null;
        NotifyStateChanged();
    }
}
