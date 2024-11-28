using EmployeeManagementSystem.BaseLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.ServerLibrary.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        // General Departments / Departments / Branches
        public DbSet<GeneralDepartment> GeneralDepartments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Branch> Branches { get; set; }

        // Country / City / Town
        //public DbSet<Country> Countries { get; set; }
        //public DbSet<City> Cities { get; set; }
        //public DbSet<Town> Towns { get; set; }

        // Authentication / Roles / User / UserRole / RefreshToken
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<SystemRole> SystemRoles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RefreshTokenInfo> RefreshTokenInfos { get; set; }

        // Other / Vacation / Overtime / Sanction / Doctor
        public DbSet<VacationType> VacationTypes { get; set; }
        public DbSet<Vacation> Vacations { get; set; }
        public DbSet<OvertimeType> OvertimeTypes { get; set; }
        public DbSet<Overtime> Overtimes { get; set; }
        public DbSet<SanctionType> SanctionTypes { get; set; }
        public DbSet<Sanction> Sanctions { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
    }
}
