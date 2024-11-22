using System.Text.Json.Serialization;

namespace EmployeeManagementSystem.BaseLibrary.Entities
{
    public class SanctionType : BaseEntity
    {
        [JsonIgnore]
        public List<Sanction>? Sanctions { get; set; }
    }
}
