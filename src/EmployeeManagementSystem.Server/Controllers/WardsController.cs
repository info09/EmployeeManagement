using EmployeeManagementSystem.ServerLibrary.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WardsController : ControllerBase
    {
        [HttpGet("ReadFile/{provinceId}/{districtId}")]
        public async Task<IActionResult> ReadFile(string provinceId, string districtId)
        {
            var result = await ReadFileJson.ReadFile("vietnamAddress.json", provinceId, districtId);

            return Ok(result);
        }
    }
}
