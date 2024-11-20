using EmployeeManagementSystem.BaseLibrary.Dtos;
using EmployeeManagementSystem.BaseLibrary.Response;
using EmployeeManagementSystem.ClientLibrary.Helpers;
using EmployeeManagementSystem.ClientLibrary.Services.Contracts;
using System.Net.Http.Json;

namespace EmployeeManagementSystem.ClientLibrary.Services.Implementations
{
    public class UserAccountService : IUserAccountService
    {
        private readonly GetHttpClient _httpClient;
        private const string AuthUrl = "api/auth";
        public UserAccountService(GetHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GeneralResponse> CreateAsync(RegisterRequest request)
        {
            var httpClient = _httpClient.GetPublicHttpClient();
            var result = await httpClient.PostAsJsonAsync($"{AuthUrl}/register", request);
            if (!result.IsSuccessStatusCode) return new GeneralResponse(false, "Error creating user");

            return await result.Content.ReadFromJsonAsync<GeneralResponse>()!;
        }

        public async Task<LoginResponse> RefreshToken(RefreshTokenRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<LoginResponse> SignInAsync(LoginRequest request)
        {
            var httpClient = _httpClient.GetPublicHttpClient();
            var result = await httpClient.PostAsJsonAsync($"{AuthUrl}/login", request);
            if (!result.IsSuccessStatusCode) return new LoginResponse(false, "Error logging in");

            return await result.Content.ReadFromJsonAsync<LoginResponse>()!;
        }
    }
}
