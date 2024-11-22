using EmployeeManagementSystem.BaseLibrary.Entities;
using EmployeeManagementSystem.BaseLibrary.Response;
using EmployeeManagementSystem.ServerLibrary.Data;
using EmployeeManagementSystem.ServerLibrary.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.ServerLibrary.Repositories.Implementations
{
    public class EmployeeRepository : IGenericRepository<Employee>
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GeneralResponse> Delete(int id)
        {
            var item = await _context.Branches.FindAsync(id);
            if (item == null)
                return NotFound();

            _context.Branches.Remove(item);
            await _context.SaveChangesAsync();
            return Success();
        }

        public async Task<List<Employee>> GetAll()
        {
            var result = await _context.Employees.AsNoTracking()
                .Include(i => i.Town)
                .ThenInclude(i => i.City)
                .ThenInclude(i => i.Country)
                .Include(i => i.Branch)
                .ThenInclude(i => i.Department)
                .ThenInclude(i => i.GeneralDepartment)
                .ToListAsync();
            return result!;
        }

        public async Task<Employee> GetById(int id)
        {
            var result = await _context.Employees
                .Include(i => i.Town)
                .ThenInclude(i => i.City)
                .ThenInclude(i => i.Country)
                .Include(i => i.Branch)
                .ThenInclude(i => i.Department)
                .ThenInclude(i => i.GeneralDepartment)
                .FirstOrDefaultAsync(i => i.Id == id);
            return result!;
        }

        public async Task<GeneralResponse> Insert(Employee entity)
        {
            var check = await CheckName(entity.Name!);
            if (check)
                return new GeneralResponse(false, "Name already exist");

            await _context.Employees.AddAsync(entity);
            await _context.SaveChangesAsync();
            return Success();
        }

        public async Task<GeneralResponse> Update(Employee entity)
        {
            var item = await _context.Employees.FindAsync(entity.Id);
            if (item == null)
                return NotFound();

            item.Name = entity.Name;
            item.Other = entity.Other;
            item.Address = entity.Address;
            item.TelephoneNumber = entity.TelephoneNumber;
            item.BranchId = entity.BranchId;
            item.TownId = entity.TownId;
            item.CivilId = entity.CivilId;
            item.FileNumber = entity.FileNumber;
            item.JobName = entity.JobName;
            item.Photo = entity.Photo;

            _context.SaveChanges();
            return Success();
        }

        private static GeneralResponse NotFound() => new GeneralResponse(false, "Not found");
        private static GeneralResponse Success() => new GeneralResponse(true, "Success");
        private async Task<bool> CheckName(string name) => await _context.Branches.AnyAsync(x => x.Name == name);
    }
}
