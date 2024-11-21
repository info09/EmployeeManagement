using EmployeeManagementSystem.BaseLibrary.Entities;
using EmployeeManagementSystem.ServerLibrary.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : GenericController<Branch>
    {
        public BranchController(IGenericRepository<Branch> genericRepository) : base(genericRepository)
        {
        }
    }
}
