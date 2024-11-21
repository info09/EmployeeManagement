namespace EmployeeManagementSystem.BaseLibrary.Entities
{
    public class Branch : BaseEntity
    {
        // Many to one relationship with Department
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

        // Ont to many relationship with Employee
        public List<Employee>? Employees { get; set; }
    }
}
