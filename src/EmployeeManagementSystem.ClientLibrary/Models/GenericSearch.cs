using EmployeeManagementSystem.BaseLibrary.SeedWorks;

namespace EmployeeManagementSystem.ClientLibrary.Models
{
    public class GenericSearch : PagingParameters
    {
        public string? Keyword { get; set; }
    }
}
