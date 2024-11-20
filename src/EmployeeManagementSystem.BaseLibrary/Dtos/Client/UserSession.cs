namespace EmployeeManagementSystem.BaseLibrary.Dtos.Client
{
    public class UserSession
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}
