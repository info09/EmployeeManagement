using EmployeeManagementSystem.BaseLibrary.Dtos;
using EmployeeManagementSystem.BaseLibrary.Response;

namespace EmployeeManagementSystem.ServerLibrary.Repositories.Contracts
{
    public interface IUserAccountRepository
    {
        Task<GeneralResponse> CreateAsync(Register register);
        Task<LoginResponse> SignInAsync(Login login);
    }
}
