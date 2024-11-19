﻿using EmployeeManagementSystem.BaseLibrary.Dtos;
using EmployeeManagementSystem.ServerLibrary.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserAccountRepository _userAccountRepository;

        public AuthController(IUserAccountRepository userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest register)
        {
            if (register == null) return BadRequest("Model is empty");
            var result = await _userAccountRepository.CreateAsync(register);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            if (login == null) return BadRequest("Model is empty");
            var result = await _userAccountRepository.SignInAsync(login);
            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> Refresh([FromBody]RefreshTokenRequest refreshToken)
        {
            if(refreshToken == null) return BadRequest("Model is empty");
            var result =  await _userAccountRepository.RefreshTokenAsync(refreshToken);
            return Ok(result);
        }
    }
}
