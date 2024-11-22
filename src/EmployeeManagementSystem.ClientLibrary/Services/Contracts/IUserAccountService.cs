using EmployeeManagementSystem.BaseLibrary.Dtos;
using EmployeeManagementSystem.BaseLibrary.Entities;
using EmployeeManagementSystem.BaseLibrary.Response;

namespace EmployeeManagementSystem.ClientLibrary.Services.Contracts
{
    public interface IUserAccountService
    {
        Task<GeneralResponse> CreateAsync(RegisterRequest request);
        Task<LoginResponse> SignInAsync(LoginRequest request);
        Task<LoginResponse> RefreshToken(RefreshTokenRequest request);
        Task<List<ManageUser>> GetUsers();
        Task<List<SystemRole>> GetRoles();
        Task<GeneralResponse> UpdateUser(ManageUser user);
        Task<GeneralResponse> DeleteUser(int id);
    }
}
