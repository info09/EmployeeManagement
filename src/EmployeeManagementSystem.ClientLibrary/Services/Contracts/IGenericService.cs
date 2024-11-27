using EmployeeManagementSystem.BaseLibrary.Response;
using EmployeeManagementSystem.BaseLibrary.SeedWorks;
using EmployeeManagementSystem.ClientLibrary.Models;

namespace EmployeeManagementSystem.ClientLibrary.Services.Contracts
{
    public interface IGenericService<T> where T : class
    {
        Task<PagedList<T>> GetAllPaging(string baseUrl, GenericSearch search);
        Task<List<T>> GetAll(string baseUrl);
        Task<T> GetById(string baseUrl, int id);
        Task<GeneralResponse> Insert(string baseUrl, T entity);
        Task<GeneralResponse> Update(string baseUrl, T entity);
        Task<GeneralResponse> Delete(string baseUrl, int id);
    }
}
