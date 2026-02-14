using Blazored.LocalStorage;
using Fallaicious.App.Models;

namespace Fallaicious.App.Services;

public class HistoryService
{
    private const string StorageKey = "fallaicious_history";
    private readonly ILocalStorageService _storage;

    public HistoryService(ILocalStorageService storage)
    {
        _storage = storage;
    }

    public async Task<List<AnalysisRequest>> GetAllAsync()
    {
        return await _storage.GetItemAsync<List<AnalysisRequest>>(StorageKey)
            ?? [];
    }

    public async Task SaveAsync(AnalysisRequest request)
    {
        var list = await GetAllAsync();
        var existing = list.FindIndex(x => x.Id == request.Id);
        if (existing >= 0)
            list[existing] = request;
        else
            list.Insert(0, request);

        await _storage.SetItemAsync(StorageKey, list);
    }

    public async Task DeleteAsync(string id)
    {
        var list = await GetAllAsync();
        list.RemoveAll(x => x.Id == id);
        await _storage.SetItemAsync(StorageKey, list);
    }
}
