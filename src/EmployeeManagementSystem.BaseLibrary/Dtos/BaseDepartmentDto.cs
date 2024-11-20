using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.BaseLibrary.Dtos
{
    public class BaseDepartmentDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string? Name { get; set; }
    }
}
