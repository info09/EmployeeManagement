using EmployeeManagementSystem.BaseLibrary.SeedWorks;
using EmployeeManagementSystem.ServerLibrary.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController<T> : ControllerBase where T : class
    {
        private readonly IGenericRepository<T> _genericRepository;

        public GenericController(IGenericRepository<T> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging(int? branchId, string? keyword, [FromQuery] PagingParameters parameters)
        {
            return Ok(await _genericRepository.GetAllPaging(branchId, keyword, parameters));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _genericRepository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _genericRepository.GetById(id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _genericRepository.Delete(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] T model)
        {
            return Ok(await _genericRepository.Insert(model));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] T model)
        {
            return Ok(await _genericRepository.Update(model));
        }
    }
}
