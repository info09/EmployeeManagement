using EmployeeManagementSystem.BaseLibrary.Entities;
using EmployeeManagementSystem.ServerLibrary.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : GenericController<Department>
    {
        public DepartmentController(IGenericRepository<Department> genericRepository) : base(genericRepository)
        {
        }
    }
}
