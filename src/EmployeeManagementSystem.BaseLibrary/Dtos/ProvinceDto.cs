namespace EmployeeManagementSystem.BaseLibrary.Dtos
{
    public class ProvinceDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }

        public ProvinceDto(string? id, string? name)
        {
            Id = id;
            Name = name;
        }
    }
}
