using EmployeeManagementSystem.BaseLibrary.Dtos.Client;
using EmployeeManagementSystem.ClientLibrary.Services.Contracts;

namespace EmployeeManagementSystem.ClientLibrary.Helpers
{
    public class CustomHttpHandler : DelegatingHandler
    {
        private readonly LocalStorageService _localStorageService;
        private readonly GetHttpClient _getHttpClient;
        private readonly IUserAccountService _userAccountService;

        public CustomHttpHandler(LocalStorageService localStorageService, GetHttpClient getHttpClient, IUserAccountService userAccountService)
        {
            _localStorageService = localStorageService;
            _getHttpClient = getHttpClient;
            _userAccountService = userAccountService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            bool loginUrl = request.RequestUri!.AbsoluteUri.Contains("login");
            bool registerUrl = request.RequestUri!.AbsoluteUri.Contains("register");
            bool refreshTokenUrl = request.RequestUri!.AbsoluteUri.Contains("refresh-token");

            if (loginUrl || registerUrl || refreshTokenUrl)
                return await base.SendAsync(request, cancellationToken);

            var result = await base.SendAsync(request, cancellationToken);
            if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                var stringToken = await _localStorageService.GetToken();
                if (stringToken == null) return result;
                string token = string.Empty;
                try
                {
                    token = request.Headers.Authorization!.Parameter!;
                }
                catch { }
                var deserializeToken = Serializations.DeserializeJsonString<UserSession>(stringToken);
                if (deserializeToken == null) return result;
                if (string.IsNullOrEmpty(token))
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", deserializeToken.Token);
                    result = await base.SendAsync(request, cancellationToken);
                }
                var newJwtToken = await GetRefreshToken(deserializeToken.RefreshToken!);
                if (string.IsNullOrEmpty(newJwtToken)) return result;

                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", newJwtToken);
                return await base.SendAsync(request, cancellationToken);
            }
            return result;
        }

        private async Task<string> GetRefreshToken(string refreshToken)
        {
            var result = await _userAccountService.RefreshToken(new BaseLibrary.Dtos.RefreshTokenRequest() { RefreshToken = refreshToken });
            string serializeToken = Serializations.SerializeObj(new UserSession() { Token = result.token, RefreshToken = result.refreshToken });
            await _localStorageService.SetToken(serializeToken);
            return result.token;
        }
    }
}
