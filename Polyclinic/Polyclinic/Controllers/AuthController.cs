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

        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <param name="registerDto">Данные для регистрации</param>
        /// <returns>JWT-токен и роль пользователя</returns>
        /// <response code="200">Пользователь успешно зарегистрирован</response>
        /// <response code="400">Пользователь с таким именем уже существует</response>
        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto>> Register(RegisterDto registerDto)
        {
            if (await _context.Users.AnyAsync(u => u.Username == registerDto.Username))
            {
                return BadRequest("Пользователь с таким именем уже существует.");
            }

            var passwordHash = Convert.ToBase64String(Encoding.UTF8.GetBytes(registerDto.Password)); // Заглушка

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

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="loginDto">Данные для входа</param>
        /// <returns>JWT-токен и роль пользователя</returns>
        /// <response code="200">Авторизация прошла успешно</response>
        /// <response code="400">Неверное имя пользователя или пароль</response>
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(LoginDto loginDto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == loginDto.Username);

            if (user == null)
            {
                return BadRequest("Неверное имя пользователя или пароль.");
            }

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
