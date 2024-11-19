using Blazored.LocalStorage;

namespace EmployeeManagementSystem.ClientLibrary.Helpers
{
    public class LocalStorageService
    {
        private readonly ILocalStorageService _localStorageService;
        private const string StorageKey = "authentication-token";

        public LocalStorageService(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async Task<string> GetToken() => await _localStorageService.GetItemAsStringAsync(StorageKey);
        public async Task SetToken(string token) => await _localStorageService.SetItemAsStringAsync(StorageKey, token);
        public async Task RemoveToken() => await _localStorageService.RemoveItemAsync(StorageKey);
    }
}
