﻿using EmployeeManagementSystem.BaseLibrary.Entities;
using EmployeeManagementSystem.BaseLibrary.Response;
using EmployeeManagementSystem.ServerLibrary.Data;
using EmployeeManagementSystem.ServerLibrary.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.ServerLibrary.Repositories.Implementations
{
    public class CityRepository : IGenericRepository<City>
    {
        private readonly ApplicationDbContext _context;

        public CityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GeneralResponse> Delete(int id)
        {
            var item = await _context.Cities.FindAsync(id);
            if (item == null)
                return NotFound();

            _context.Cities.Remove(item);
            await _context.SaveChangesAsync();
            return Success();
        }

        public async Task<List<City>> GetAll() => await _context.Cities.AsNoTracking().Include(i => i.Country).ToListAsync();

        public async Task<City> GetById(int id) => await _context.Cities.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<GeneralResponse> Insert(City entity)
        {
            var check = await CheckName(entity.Name!);
            if (check)
                return new GeneralResponse(false, "Name already exist");

            await _context.Cities.AddAsync(entity);
            await _context.SaveChangesAsync();
            return Success();
        }

        public async Task<GeneralResponse> Update(City entity)
        {
            var item = await _context.Cities.FindAsync(entity.Id);
            if (item == null)
                return NotFound();

            var check = await CheckName(entity.Name!);
            if (check)
                return new GeneralResponse(false, "Name already exist");

            item.Name = entity.Name;
            item.CountryId = entity.CountryId;
            _context.SaveChanges();
            return Success();
        }

        private static GeneralResponse NotFound() => new GeneralResponse(false, "Not found");
        private static GeneralResponse Success() => new GeneralResponse(true, "Success");
        private async Task<bool> CheckName(string name) => await _context.Cities.AnyAsync(x => x.Name == name);
    }
}
