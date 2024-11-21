using EmployeeManagementSystem.BaseLibrary.Entities;
using EmployeeManagementSystem.ServerLibrary.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : GenericController<City>
    {
        public CityController(IGenericRepository<City> genericRepository) : base(genericRepository)
        {
        }
    }
}
