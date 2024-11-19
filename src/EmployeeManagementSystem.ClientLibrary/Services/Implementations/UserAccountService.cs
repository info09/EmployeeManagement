using EmployeeManagementSystem.BaseLibrary.Dtos;
using EmployeeManagementSystem.BaseLibrary.Response;
using EmployeeManagementSystem.ClientLibrary.Services.Contracts;

namespace EmployeeManagementSystem.ClientLibrary.Services.Implementations
{
    public class UserAccountService : IUserAccountService
    {
        public Task<GeneralResponse> CreateAsync(RegisterRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<LoginResponse> RefreshToken(RefreshTokenRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<LoginResponse> SignInAsync(LoginRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
