using EmployeeManagementSystem.BaseLibrary.Entities;
using EmployeeManagementSystem.BaseLibrary.Response;
using EmployeeManagementSystem.BaseLibrary.SeedWorks;
using EmployeeManagementSystem.ServerLibrary.Data;
using EmployeeManagementSystem.ServerLibrary.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.ServerLibrary.Repositories.Implementations
{
    public class BranchRepository : IGenericRepository<Branch>
    {
        private readonly ApplicationDbContext _context;

        public BranchRepository(ApplicationDbContext context)
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

        public async Task<List<Branch>> GetAll() => await _context.Branches.AsNoTracking().Include(i => i.Department).ToListAsync();

        public async Task<Branch> GetById(int id) => await _context.Branches.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<GeneralResponse> Insert(Branch entity)
        {
            var check = await CheckName(entity.Name!);
            if (check)
                return new GeneralResponse(false, "Name already exist");

            await _context.Branches.AddAsync(entity);
            await _context.SaveChangesAsync();
            return Success();
        }

        public async Task<GeneralResponse> Update(Branch entity)
        {
            var item = await _context.Branches.FindAsync(entity.Id);
            if (item == null)
                return NotFound();

            var check = await CheckName(entity.Name!);
            if (check && item.DepartmentId == entity.DepartmentId)
                return new GeneralResponse(false, "Name already exist");

            item.Name = entity.Name;
            item.DepartmentId = entity.DepartmentId;
            _context.SaveChanges();
            return Success();
        }

        private static GeneralResponse NotFound() => new GeneralResponse(false, "Not found");
        private static GeneralResponse Success() => new GeneralResponse(true, "Success");
        private async Task<bool> CheckName(string name) => await _context.Branches.AnyAsync(x => x.Name == name);

        public async Task<PagedList<Branch>> GetAllPaging(string? keyword, PagingParameters pagingParameters)
        {
            var query = _context.Branches.AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(i => i.Name.ToLower().Contains(keyword.ToLower()));

            var data = await query.Skip((pagingParameters.PageNumber - 1) * pagingParameters.PageSize).Take(pagingParameters.PageSize).ToListAsync();
            var count = await query.CountAsync();
            return new PagedList<Branch>(data, count, pagingParameters.PageNumber, pagingParameters.PageSize);
        }
    }
}
