namespace EmployeeManagementSystem.BaseLibrary.VietNam
{
    public class District
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public List<Ward>? Wards { get; set; }
    }
}
