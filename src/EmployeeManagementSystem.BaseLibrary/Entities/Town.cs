using System.Text.Json.Serialization;

namespace EmployeeManagementSystem.BaseLibrary.Entities
{
    public class Town : BaseEntity
    {
        public int CityId { get; set; }
        public City? City { get; set; }
        [JsonIgnore]
        public List<Employee>? Employees { get; set; }
    }
}
