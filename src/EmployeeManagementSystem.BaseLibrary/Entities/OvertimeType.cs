using System.Text.Json.Serialization;

namespace EmployeeManagementSystem.BaseLibrary.Entities
{
    public class OvertimeType : BaseEntity
    {
        [JsonIgnore]
        public List<Overtime>? Overtimes { get; set; }
    }
}
