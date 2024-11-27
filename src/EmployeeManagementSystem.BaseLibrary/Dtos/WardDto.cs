namespace EmployeeManagementSystem.BaseLibrary.Dtos
{
    public class WardDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Level { get; set; }

        public WardDto(string? id, string? name, string? level)
        {
            Id = id;
            Name = name;
            Level = level;
        }
    }
}
