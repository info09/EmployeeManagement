using EmployeeManagementSystem.BaseLibrary.Entities;
using EmployeeManagementSystem.BaseLibrary.Response;
using EmployeeManagementSystem.BaseLibrary.SeedWorks;
using EmployeeManagementSystem.ServerLibrary.Data;
using EmployeeManagementSystem.ServerLibrary.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.ServerLibrary.Repositories.Implementations
{
    public class DepartmentRepository : IGenericRepository<Department>
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GeneralResponse> Delete(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
                return NotFound();

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return Success();
        }

        public async Task<List<Department>> GetAll() => await _context.Departments.AsNoTracking().Include(i => i.GeneralDepartment).ToListAsync();

        public async Task<Department> GetById(int id) => await _context.Departments.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<GeneralResponse> Insert(Department entity)
        {
            var check = await CheckName(entity.Name!);
            if (check)
                return new GeneralResponse(false, "Name already exist");

            await _context.Departments.AddAsync(entity);
            await _context.SaveChangesAsync();
            return Success();
        }

        public async Task<GeneralResponse> Update(Department entity)
        {
            var department = await _context.Departments.FindAsync(entity.Id);
            if (department == null)
                return NotFound();
            var check = await CheckName(entity.Name!);
            if (check && department.GeneralDepartmentId == entity.GeneralDepartmentId)
                return new GeneralResponse(false, "Name already exist");

            department.Name = entity.Name;
            department.GeneralDepartmentId = entity.GeneralDepartmentId;
            _context.SaveChanges();
            return Success();
        }

        private static GeneralResponse NotFound() => new GeneralResponse(false, "Not found");
        private static GeneralResponse Success() => new GeneralResponse(true, "Success");
        private async Task<bool> CheckName(string name) => await _context.Departments.AnyAsync(x => x.Name == name);

        public async Task<PagedList<Department>> GetAllPaging(string? keyword, PagingParameters pagingParameters)
        {
            var query = _context.Departments.Include(i => i.GeneralDepartment).AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(i => i.Name.ToLower().Contains(keyword.ToLower()));

            var data = await query.Skip((pagingParameters.PageNumber - 1) * pagingParameters.PageSize).Take(pagingParameters.PageSize).ToListAsync();
            var count = await query.CountAsync();
            return new PagedList<Department>(data, count, pagingParameters.PageNumber, pagingParameters.PageSize);
        }
    }
}
