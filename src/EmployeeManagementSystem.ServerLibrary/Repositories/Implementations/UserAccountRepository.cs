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

        public async Task<GeneralResponse> CreateAsync(RegisterRequest request)
        {
            if (request is null) return new GeneralResponse(false, "Registration failed");

            var checkUser = await FindUserByEmaiAsync(request.Email!);
            if (checkUser != null) return new GeneralResponse(false, "User already exist");

            // Save User
            var appUser = await AddToDb(new ApplicationUser()
            {
                FullName = request.FullName,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password!),
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

        public async Task<LoginResponse> SignInAsync(LoginRequest request)
        {
            if (request == null)
                return new LoginResponse(false, "Model is null");

            var appUser = await FindUserByEmaiAsync(request.Email!);
            if (appUser == null)
                return new LoginResponse(false, "User not found");

            if (!BCrypt.Net.BCrypt.Verify(request.Password!, appUser.Password!))
                return new LoginResponse(false, "Password is incorrect");

            var getUserRole = await FindUserRoleAsync(appUser.Id);
            if (getUserRole == null)
                return new LoginResponse(false, "User role not found");

            var getRole = await FindRoleAsync(getUserRole.RoleId);
            if (getRole == null)
                return new LoginResponse(false, "Role not found");

            string jwtToken = GenerateToken(appUser, getRole!.Name!);
            string refreshToken = GenerateRefreshToken();

            var findUser = await _context.RefreshTokenInfos.FirstOrDefaultAsync(i => i.UserId == appUser.Id);
            if (findUser != null)
            {
                findUser!.Token = refreshToken;
                await _context.SaveChangesAsync();
            }
            else
            {
                await AddToDb(new RefreshTokenInfo() { Token = refreshToken, UserId = appUser.Id });
            }

            return new LoginResponse(true, "Login success", jwtToken, refreshToken);
        }

        private async Task<ApplicationUser> FindUserByEmaiAsync(string email) => await _context.ApplicationUsers.FirstOrDefaultAsync(x => x.Email!.ToLower().Equals(email!.ToLower()));
        private async Task<UserRole> FindUserRoleAsync(int userId) => await _context.UserRoles.FirstOrDefaultAsync(x => x.UserId == userId);
        private async Task<SystemRole> FindRoleAsync(int roleId) => await _context.SystemRoles.FirstOrDefaultAsync(x => x.Id == roleId);

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

        public async Task<LoginResponse> RefreshTokenAsync(RefreshTokenRequest request)
        {
            if (request == null)
                return new LoginResponse(false, "Model is null");

            var checkToken = await _context.RefreshTokenInfos.FirstOrDefaultAsync(i => i.Token!.Equals(request.RefreshToken!));
            if (checkToken == null)
                return new LoginResponse(false, "Refresh token not found");

            //Get UserDetails
            var appUser = await _context.ApplicationUsers.FirstOrDefaultAsync(i => i.Id == checkToken.UserId);

            var userRole = await FindUserRoleAsync(appUser!.Id);
            var getRole = await FindRoleAsync(userRole!.RoleId);
            string jwtToken = GenerateToken(appUser!, getRole!.Name!);
            string refreshToken = GenerateRefreshToken();

            var updateRefreshToken = await _context.RefreshTokenInfos.FirstOrDefaultAsync(i => i.UserId == appUser.Id);
            if (updateRefreshToken == null)
                return new LoginResponse(false, "Refresh token not found");

            updateRefreshToken.Token = refreshToken;
            await _context.SaveChangesAsync();

            return new LoginResponse(true, "Refresh token success", jwtToken, refreshToken);
        }

        public async Task<List<ManageUser>> GetUsers()
        {
            var allUsers = await _context.ApplicationUsers.AsNoTracking().ToListAsync();
            var allUserRoles = await _context.UserRoles.AsNoTracking().ToListAsync();
            var allRoles = await _context.SystemRoles.AsNoTracking().ToListAsync();

            if (allUsers.Count == 0 || allUserRoles.Count == 0 || allRoles.Count == 0)
                return null!;

            var users = (from u in allUsers
                         join ur in allUserRoles on u.Id equals ur.UserId
                         join r in allRoles on ur.RoleId equals r.Id
                         select new ManageUser
                         {
                             UserId = u.Id,
                             Email = u.Email,
                             Name = u.FullName,
                             Role = r.Name
                         }).ToList();

            return users;
        }

        public async Task<List<SystemRole>> GetRoles() => await _context.SystemRoles.AsNoTracking().ToListAsync();

        public async Task<GeneralResponse> UpdateUser(ManageUser user)
        {
            var getRole = await _context.SystemRoles.FirstOrDefaultAsync(i => i.Name!.Equals(user.Role!));
            var userRole = await _context.UserRoles.FirstOrDefaultAsync(i => i.UserId == user.UserId);
            userRole!.RoleId = getRole!.Id;
            await _context.SaveChangesAsync();
            return new GeneralResponse(true, "Update user successfully");
        }

        public async Task<GeneralResponse> DeleteUser(int id)
        {
            var user = await _context.ApplicationUsers.FirstOrDefaultAsync(i => i.Id == id);
            _context.ApplicationUsers.Remove(user!);
            await _context.SaveChangesAsync();
            return new GeneralResponse(true, "Delete user successfully");
        }
    }
}
