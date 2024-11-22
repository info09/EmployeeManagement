using System.Text.Json.Serialization;

namespace EmployeeManagementSystem.BaseLibrary.Entities
{
    public class City : BaseEntity
    {
        public int CountryId { get; set; }
        public Country? Country { get; set; }
        [JsonIgnore]
        public List<Town>? Towns { get; set; }
    }
}
