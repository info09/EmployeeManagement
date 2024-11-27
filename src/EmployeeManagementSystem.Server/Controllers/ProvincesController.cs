using EmployeeManagementSystem.ServerLibrary.Helper;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvincesController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> ReadFile()
        {
            var result = await ReadFileJson.ReadFile("vietnamAddress.json");

            return Ok(result);
        }
    }
}
