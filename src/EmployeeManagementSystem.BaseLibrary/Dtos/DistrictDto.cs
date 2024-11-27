namespace EmployeeManagementSystem.BaseLibrary.Dtos
{
    public class DistrictDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }

        public DistrictDto(string? id, string? name)
        {
            Id = id;
            Name = name;
        }
    }
}
