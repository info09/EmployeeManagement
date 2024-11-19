namespace EmployeeManagementSystem.BaseLibrary.Response
{
    public record LoginResponse(bool flag, string message = null!, string token = null!, string refreshToken = null!);
}
