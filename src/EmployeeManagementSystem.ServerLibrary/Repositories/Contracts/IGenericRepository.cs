using EmployeeManagementSystem.BaseLibrary.Response;

namespace EmployeeManagementSystem.ServerLibrary.Repositories.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        Task<GeneralResponse> Insert(T entity);
        Task<GeneralResponse> Update(T entity);
        Task<GeneralResponse> Delete(int entity);
    }
}
