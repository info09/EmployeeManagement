using EmployeeManagementSystem.BaseLibrary.Dtos.Client;

namespace EmployeeManagementSystem.ClientLibrary.Helpers
{
    public class GetHttpClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly LocalStorageService _storageService;
        private const string HeaderKey = "Authorization";

        public GetHttpClient(IHttpClientFactory httpClientFactory, LocalStorageService storageService)
        {
            _httpClientFactory = httpClientFactory;
            _storageService = storageService;
        }

        public async Task<HttpClient> GetPrivateHttpClient()
        {
            var client = _httpClientFactory.CreateClient("SystemApiClient");
            var token = await _storageService.GetToken();
            if (string.IsNullOrEmpty(token)) return client;

            var deserializeToken = Serializations.DeserializeJsonString<UserSession>(token);
            if (deserializeToken == null) return client;
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", deserializeToken.Token);
            return client;
        }

        public HttpClient GetPublicHttpClient()
        {
            var client = _httpClientFactory.CreateClient("SystemApiClient");
            client.DefaultRequestHeaders.Remove(HeaderKey);
            return client;
        }
    }
}
