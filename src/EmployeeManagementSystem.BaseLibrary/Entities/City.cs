namespace EmployeeManagementSystem.BaseLibrary.Entities
{
    public class City : BaseEntity
    {
        public int CountryId { get; set; }
        public Country? Country { get; set; }

        public List<Town>? Towns { get; set; }
    }
}
