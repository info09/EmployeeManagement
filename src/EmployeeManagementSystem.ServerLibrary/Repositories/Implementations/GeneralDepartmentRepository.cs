using EmployeeManagementSystem.BaseLibrary.Entities;
using EmployeeManagementSystem.BaseLibrary.Response;
using EmployeeManagementSystem.ServerLibrary.Data;
using EmployeeManagementSystem.ServerLibrary.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.ServerLibrary.Repositories.Implementations
{
    public class GeneralDepartmentRepository : IGenericRepository<GeneralDepartment>
    {
        private readonly ApplicationDbContext _context;

        public GeneralDepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GeneralResponse> Delete(int id)
        {
            var generalDepartment = await _context.GeneralDepartments.FindAsync(id);
            if (generalDepartment == null)
                return NotFound();

            _context.GeneralDepartments.Remove(generalDepartment);
            await _context.SaveChangesAsync();
            return Success();
        }

        public async Task<List<GeneralDepartment>> GetAll() => await _context.GeneralDepartments.ToListAsync();

        public async Task<GeneralDepartment> GetById(int id) => await _context.GeneralDepartments.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<GeneralResponse> Insert(GeneralDepartment entity)
        {
            var check = await CheckName(entity.Name!);
            if (check)
                return new GeneralResponse(false, "Name already exist");

            await _context.GeneralDepartments.AddAsync(entity);
            await _context.SaveChangesAsync();
            return Success();
        }

        public async Task<GeneralResponse> Update(GeneralDepartment entity)
        {
            var generalDepartment = await _context.GeneralDepartments.FindAsync(entity.Id);
            if (generalDepartment == null)
                return NotFound();

            generalDepartment.Name = entity.Name;
            _context.SaveChanges();
            return Success();
        }

        private static GeneralResponse NotFound() => new GeneralResponse(false, "Not found");
        private static GeneralResponse Success() => new GeneralResponse(true, "Success");
        private async Task<bool> CheckName(string name) => await _context.GeneralDepartments.AnyAsync(x => x.Name == name);
    }
}
