using EmployeeManagementSystem.BaseLibrary.Entities;
using EmployeeManagementSystem.ServerLibrary.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : GenericController<Country>
    {
        public CountryController(IGenericRepository<Country> genericRepository) : base(genericRepository)
        {
        }
    }
}
