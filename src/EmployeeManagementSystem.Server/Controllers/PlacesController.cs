using EmployeeManagementSystem.ServerLibrary.Helper;

using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        [HttpGet("getProvinces")]
        public async Task<IActionResult> GetProvince()
        {
            var result = await ReadFileJson.ReadFileAsync("vietnamAddress.json");
            return Ok(result);
        }

        [HttpGet("getDistricts/{provinceId}")]
        public async Task<IActionResult> GetDistrict(string provinceId)
        {
            var result = await ReadFileJson.ReadFileAsync("vietnamAddress.json", provinceId);
            return Ok(result);
        }

        [HttpGet("getWards/{provinceId}/{districtId}")]
        public async Task<IActionResult> GetWards(string provinceId, string districtId)
        {
            var result = await ReadFileJson.ReadFileAsync("vietnamAddress.json", provinceId, districtId);
            return Ok(result);
        }
    }
}
