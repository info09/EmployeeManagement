namespace EmployeeManagementSystem.BaseLibrary.Entities
{
    public class Town : BaseEntity
    {
        public int CityId { get; set; }
        public City? City { get; set; }

        public List<Employee>? Employees { get; set; }
    }
}
