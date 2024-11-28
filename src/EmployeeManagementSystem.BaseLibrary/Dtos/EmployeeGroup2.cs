using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.BaseLibrary.Dtos
{
    public class EmployeeGroup2
    {
        [Required]
        public string? JobName { get; set; }
        [Required, Range(1, 99999, ErrorMessage = "You must select branch")]
        public int BranchId { get; set; }
        public string? TownId { get; set; }
        [Required]
        public string? Other { get; set; }
        [Required]
        public string Address { get; set; } = string.Empty;
    }
}
