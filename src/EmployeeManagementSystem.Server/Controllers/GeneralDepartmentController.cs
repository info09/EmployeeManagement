using EmployeeManagementSystem.BaseLibrary.Entities;
using EmployeeManagementSystem.ServerLibrary.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralDepartmentController : GenericController<GeneralDepartment>
    {
        public GeneralDepartmentController(IGenericRepository<GeneralDepartment> genericRepository) : base(genericRepository)
        {
        }
    }
}
