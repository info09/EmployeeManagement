using EmployeeManagementSystem.BaseLibrary.Entities;
using EmployeeManagementSystem.ServerLibrary.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TownController : GenericController<Town>
    {
        public TownController(IGenericRepository<Town> genericRepository) : base(genericRepository)
        {
        }
    }
}
