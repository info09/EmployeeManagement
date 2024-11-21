namespace EmployeeManagementSystem.BaseLibrary.Entities
{
    public class Country : BaseEntity
    {
        public List<City>? Cities { get; set; }
    }
}
