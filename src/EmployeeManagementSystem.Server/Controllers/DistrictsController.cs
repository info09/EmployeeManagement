using EmployeeManagementSystem.ServerLibrary.Helper;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictsController : ControllerBase
    {
        [HttpGet("{provinceId}")]
        public async Task<IActionResult> ReadFile(string provinceId)
        {
            var result = await ReadFileJson.ReadFile("vietnamAddress.json", provinceId);

            return Ok(result);
        }
    }
}
