using System.Text.Json.Serialization;

namespace EmployeeManagementSystem.BaseLibrary.Entities
{
    public class Country : BaseEntity
    {
        [JsonIgnore]
        public List<City>? Cities { get; set; }
    }
}
