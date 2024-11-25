using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.BaseLibrary.Dtos
{
    public class EmployeeGroup2
    {
        [Required]
        public string? JobName { get; set; }
        [Required, Range(1, 9999, ErrorMessage = "You must select branch")]
        public int BranchId { get; set; }
        [Required, Range(1, 9999, ErrorMessage = "You must select town")]
        public int TownId { get; set; }
        [Required]
        public string? Other { get; set; }
    }
}
