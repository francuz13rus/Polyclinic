using Polyclinic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Polyclinic.Data_Transfer_Objects;
using Polyclinic.JWT;
using Polyclinic.Models;
using System.Text;

namespace Polyclinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ClinicApiContext _context;
        private readonly JwtTokenService _jwtTokenService;

        public AuthController(ClinicApiContext context, JwtTokenService jwtTokenService)
        {
            _context = context;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto>> Register(RegisterDto registerDto)
        {
            // Проверка, существует ли пользователь
            if (await _context.Users.AnyAsync(u => u.Username == registerDto.Username))
            {
                return BadRequest("Пользователь с таким именем уже существует.");
            }

            // Хеширование пароля (для простоты используем простой подход, в продакшене используй BCrypt или Argon2)
            var passwordHash = Convert.ToBase64String(Encoding.UTF8.GetBytes(registerDto.Password)); // Это заглушка, замени на настоящий хешировщик

            var user = new User
            {
                Username = registerDto.Username,
                PasswordHash = passwordHash,
                Role = registerDto.Role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var token = _jwtTokenService.GenerateToken(user);
            return Ok(new AuthResponseDto { Token = token, Role = user.Role });
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(LoginDto loginDto)
        {
            // Поиск пользователя
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == loginDto.Username);

            if (user == null)
            {
                return BadRequest("Неверное имя пользователя или пароль.");
            }

            // Проверка пароля (заглушка, замени на настоящий хешировщик)
            var passwordHash = Convert.ToBase64String(Encoding.UTF8.GetBytes(loginDto.Password));
            if (user.PasswordHash != passwordHash)
            {
                return BadRequest("Неверное имя пользователя или пароль.");
            }

            var token = _jwtTokenService.GenerateToken(user);
            return Ok(new AuthResponseDto { Token = token, Role = user.Role });
        }
    }
}
