using EmployeeManagementSystem.BaseLibrary.Response;
using EmployeeManagementSystem.BaseLibrary.SeedWorks;

namespace EmployeeManagementSystem.ServerLibrary.Repositories.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        Task<PagedList<T>> GetAllPaging(int? branchId, string? keyword, PagingParameters pagingParameters);
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        Task<GeneralResponse> Insert(T entity);
        Task<GeneralResponse> Update(T entity);
        Task<GeneralResponse> Delete(int entity);
    }
}
