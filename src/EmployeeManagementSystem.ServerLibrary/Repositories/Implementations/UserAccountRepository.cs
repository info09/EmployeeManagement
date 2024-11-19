using EmployeeManagementSystem.BaseLibrary.Dtos;
using EmployeeManagementSystem.BaseLibrary.Entities;
using EmployeeManagementSystem.BaseLibrary.Response;
using EmployeeManagementSystem.ServerLibrary.Data;
using EmployeeManagementSystem.ServerLibrary.Helper;
using EmployeeManagementSystem.ServerLibrary.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EmployeeManagementSystem.ServerLibrary.Repositories.Implementations
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IOptions<JwtSection> _options;

        public UserAccountRepository(ApplicationDbContext context, IOptions<JwtSection> options)
        {
            _context = context;
            _options = options;
        }

        public async Task<GeneralResponse> CreateAsync(Register register)
        {
            if (register is null) return new GeneralResponse(false, "Registration failed");

            var checkUser = await FindUserByEmaiAsync(register.Email!);
            if (checkUser != null) return new GeneralResponse(false, "User already exist");

            // Save User
            var appUser = await AddToDb(new ApplicationUser()
            {
                FullName = register.FullName,
                Email = register.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(register.Password!),
            });
            var checkAdminRole = await _context.SystemRoles.FirstOrDefaultAsync(_ => _.Name!.Equals(Constants.Admin));
            var adminRole = new SystemRole();
            if (checkAdminRole == null)
            {
                adminRole = await AddToDb(new SystemRole() { Name = Constants.Admin });
                await AddToDb(new UserRole() { RoleId = adminRole.Id, UserId = appUser.Id });

                return new GeneralResponse(true, "Create user successfully");
            }

            var checkUserRole = await _context.SystemRoles.FirstOrDefaultAsync(_ => _.Name!.Equals(Constants.User));
            var userRole = new SystemRole();
            if (checkUserRole == null)
            {
                userRole = await AddToDb(new SystemRole() { Name = Constants.User });
                await AddToDb(new UserRole() { RoleId = userRole.Id, UserId = appUser.Id });
            }
            else
            {
                await AddToDb(new UserRole() { RoleId = checkUserRole.Id, UserId = appUser.Id });
            }
            return new GeneralResponse(true, "Create user successfully");
        }

        public async Task<LoginResponse> SignInAsync(Login login)
        {
            if (login == null)
                return new LoginResponse(false, "Model is null");

            var appUser = await FindUserByEmaiAsync(login.Email!);
            if (appUser == null)
                return new LoginResponse(false, "User not found");

            if (!BCrypt.Net.BCrypt.Verify(login.Password!, appUser.Password!))
                return new LoginResponse(false, "Password is incorrect");

            var getUserRole = await _context.UserRoles.FirstOrDefaultAsync(i => i.UserId == appUser.Id);
            if (getUserRole == null)
                return new LoginResponse(false, "User role not found");

            var getRole = await _context.SystemRoles.FirstOrDefaultAsync(i => i.Id == getUserRole.RoleId);
            if (getRole == null)
                return new LoginResponse(false, "Role not found");

            string jwtToken = GenerateToken(appUser, getRole!.Name!);
            string refreshToken = GenerateRefreshToken();
            return new LoginResponse(true, "Login success", jwtToken, refreshToken);
        }

        private async Task<ApplicationUser> FindUserByEmaiAsync(string email) => await _context.ApplicationUsers.FirstOrDefaultAsync(x => x.Email!.ToLower().Equals(email!.ToLower()));

        private async Task<T> AddToDb<T>(T model)
        {
            var result = _context.Add(model!);
            await _context.SaveChangesAsync();
            return (T)result.Entity;
        }

        private string GenerateToken(ApplicationUser user, string role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Key!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Role, role!),
            };
            var token = new JwtSecurityToken(issuer: _options.Value.Issuer, audience: _options.Value.Audience, claims: userClaims, expires: DateTime.Now.AddDays(1), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
