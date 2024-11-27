using EmployeeManagementSystem.BaseLibrary.Entities;
using EmployeeManagementSystem.BaseLibrary.Response;
using EmployeeManagementSystem.BaseLibrary.SeedWorks;
using EmployeeManagementSystem.ServerLibrary.Data;
using EmployeeManagementSystem.ServerLibrary.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.ServerLibrary.Repositories.Implementations
{
    public class TownRepository : IGenericRepository<Town>
    {
        private readonly ApplicationDbContext _context;

        public TownRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GeneralResponse> Delete(int id)
        {
            var item = await _context.Towns.FindAsync(id);
            if (item == null)
                return NotFound();

            _context.Towns.Remove(item);
            await _context.SaveChangesAsync();
            return Success();
        }

        public async Task<List<Town>> GetAll() => await _context.Towns.AsNoTracking().Include(i => i.City).ToListAsync();

        public async Task<Town> GetById(int id) => await _context.Towns.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<GeneralResponse> Insert(Town entity)
        {
            var check = await CheckName(entity.Name!);
            if (check)
                return new GeneralResponse(false, "Name already exist");

            await _context.Towns.AddAsync(entity);
            await _context.SaveChangesAsync();
            return Success();
        }

        public async Task<GeneralResponse> Update(Town entity)
        {
            var item = await _context.Towns.FindAsync(entity.Id);
            if (item == null)
                return NotFound();
            var check = await CheckName(entity.Name!);
            if (check && item.CityId == entity.CityId)
                return new GeneralResponse(false, "Name already exist");

            item.Name = entity.Name;
            item.CityId = entity.CityId;
            _context.SaveChanges();
            return Success();
        }

        private static GeneralResponse NotFound() => new GeneralResponse(false, "Not found");
        private static GeneralResponse Success() => new GeneralResponse(true, "Success");
        private async Task<bool> CheckName(string name) => await _context.Towns.AnyAsync(x => x.Name == name);

        public async Task<PagedList<Town>> GetAllPaging(string? keyword, PagingParameters pagingParameters)
        {
            var query = _context.Towns.AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(i => i.Name.ToLower().Contains(keyword.ToLower()));

            var data = await query.Skip((pagingParameters.PageNumber - 1) * pagingParameters.PageSize).Take(pagingParameters.PageSize).ToListAsync();
            var count = await query.CountAsync();
            return new PagedList<Town>(data, count, pagingParameters.PageNumber, pagingParameters.PageSize);
        }
    }
}
