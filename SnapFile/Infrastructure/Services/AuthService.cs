using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using SnapFile.Application.Auth.DTOs;
using SnapFile.Application.Auth.Services;
using SnapFile.Domain.Entities;
using SnapFile.Infrastructure.Data;

namespace SnapFile.Infrastructure.Services
{
    public class AuthService
    {
        private readonly AppDbContext _db;
        private readonly JwtService _jwt;

        public AuthService(AppDbContext db, JwtService jwt)
        {
            _db = db;
            _jwt = jwt;
        }

        public async Task<string> Login(LoginDto dto)
        {
            var user = await _db.Users
                .FirstOrDefaultAsync(x => x.Phone == dto.Phone);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new Exception("Invalid credentials");

            return _jwt.GenerateToken(user);
        }

        public async Task Register(RegisterDto dto)
        {
            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                MiddleName = dto.MiddleName,
                Phone = dto.Phone,
                PositionId = dto.PositionId,
                DepartmentId = dto.DepartmentId,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();
        }

        public async Task RegisterAdmin(RegisterDto dto)
        {
            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                MiddleName = dto.MiddleName,
                Phone = dto.Phone,
                PositionId = dto.PositionId,
                DepartmentId = dto.DepartmentId,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                IsAdmin = true
            };
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
        }
    }
}
