using EmployeeManagementSystem.BaseLibrary.Entities;
using EmployeeManagementSystem.BaseLibrary.Response;
using EmployeeManagementSystem.BaseLibrary.SeedWorks;
using EmployeeManagementSystem.ServerLibrary.Data;
using EmployeeManagementSystem.ServerLibrary.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.ServerLibrary.Repositories.Implementations
{
    public class CountryRepository : IGenericRepository<Country>
    {
        private readonly ApplicationDbContext _context;

        public CountryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GeneralResponse> Delete(int id)
        {
            var item = await _context.Countries.FindAsync(id);
            if (item == null)
                return NotFound();

            _context.Countries.Remove(item);
            await _context.SaveChangesAsync();
            return Success();
        }

        public async Task<List<Country>> GetAll() => await _context.Countries.ToListAsync();

        public async Task<Country> GetById(int id) => await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<GeneralResponse> Insert(Country entity)
        {
            var check = await CheckName(entity.Name!);
            if (check)
                return new GeneralResponse(false, "Name already exist");

            await _context.Countries.AddAsync(entity);
            await _context.SaveChangesAsync();
            return Success();
        }

        public async Task<GeneralResponse> Update(Country entity)
        {
            var item = await _context.Countries.FindAsync(entity.Id);
            if (item == null)
                return NotFound();
            var check = await CheckName(entity.Name!);
            if (check)
                return new GeneralResponse(false, "Name already exist");

            item.Name = entity.Name;
            _context.SaveChanges();
            return Success();
        }

        private static GeneralResponse NotFound() => new GeneralResponse(false, "Not found");
        private static GeneralResponse Success() => new GeneralResponse(true, "Success");
        private async Task<bool> CheckName(string name) => await _context.Countries.AnyAsync(x => x.Name == name);

        public async Task<PagedList<Country>> GetAllPaging(string? keyword, PagingParameters pagingParameters)
        {
            var query = _context.Countries.AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(i => i.Name.ToLower().Contains(keyword.ToLower()));

            var data = await query.Skip((pagingParameters.PageNumber - 1) * pagingParameters.PageSize).Take(pagingParameters.PageSize).ToListAsync();
            var count = await query.CountAsync();
            return new PagedList<Country>(data, count, pagingParameters.PageNumber, pagingParameters.PageSize);
        }
    }
}
