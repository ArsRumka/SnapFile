using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using SnapFile.Application.Auth.DTOs;
using SnapFile.Application.Auth.Services;
using SnapFile.Domain.Entities;
using SnapFile.Infrastructure.Data;
using SnapFile.Application.Services.Interfaces;
using System.Security.Cryptography;

namespace SnapFile.Infrastructure.Services
{
    public class AuthService
    {
        private readonly AppDbContext _db;
        private readonly JwtService _jwt;
        private readonly IEmailService _email;

        public AuthService(AppDbContext db, JwtService jwt, IEmailService email)
        {
            _db = db;
            _jwt = jwt;
            _email = email;
        }

        public async Task<string> Login(LoginDto dto)
        {
            var user = await _db.Users
                .FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new Exception("Invalid credentials");

            if (!user.IsEmailConfirmed)
                throw new Exception("Email not confirmed");

            return _jwt.GenerateToken(user);
        }

        public async Task Register(RegisterDto dto)
        {
            var existingUser = await _db.Users
                .FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (existingUser != null)
            {
                if (existingUser.IsEmailConfirmed)
                    throw new Exception("Email already in use");

                existingUser.FirstName = dto.FirstName;
                existingUser.LastName = dto.LastName;
                existingUser.MiddleName = dto.MiddleName;
                existingUser.Phone = dto.Phone;
                existingUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            }
            else
            {
                var user = new User
                {
                    Email = dto.Email,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    MiddleName = dto.MiddleName,
                    Phone = dto.Phone,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
                };

                _db.Users.Add(user);
            }

            var code = GenerateCode();

            _db.EmailCodes.Add(new EmailCode
            {
                Email = dto.Email,
                Code = code,
                ExpireAt = DateTime.UtcNow.AddMinutes(10)
            });

            await _db.SaveChangesAsync();

            await _email.SendCode(dto.Email, code);
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
        public async Task ConfirmEmail(ConfirmEmailDto dto)
        {
            var code = await _db.EmailCodes
                .Where(x => x.Email == dto.Email && !x.IsUsed)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync();

            if (code == null || code.ExpireAt < DateTime.UtcNow)
                throw new Exception("Code expired");

            if (code.Code != dto.Code)
            {
                code.Attempts++;

                if (code.Attempts >= 5)
                    throw new Exception("Too many attempts");

                await _db.SaveChangesAsync();
                throw new Exception("Invalid code");
            }

            var user = await _db.Users.FirstAsync(x => x.Email == dto.Email);

            user.IsEmailConfirmed = true;
            code.IsUsed = true;

            await _db.SaveChangesAsync();
        }

        public async Task RequestReset(string email)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null) return;

            var code = GenerateCode();

            _db.EmailCodes.Add(new EmailCode
            {
                Email = email,
                Code = code,
                ExpireAt = DateTime.UtcNow.AddMinutes(10)
            });

            await _db.SaveChangesAsync();

            await _email.SendCode(email, code);
        }

        public async Task ResetPassword(ResetPasswordDto dto)
        {
            var code = await _db.EmailCodes
                .Where(x => x.Email == dto.Email && !x.IsUsed)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync();

            if (code == null || code.ExpireAt < DateTime.UtcNow)
                throw new Exception("Invalid or expired code");

            if (code.Code != dto.Code)
                throw new Exception("Invalid code");

            var user = await _db.Users.FirstAsync(x => x.Email == dto.Email);

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            code.IsUsed = true;

            await _db.SaveChangesAsync();
        }

        public async Task ResendCode(string email)
        {
            var lastCode = await _db.EmailCodes
                .Where(x => x.Email == email)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync();

            if (lastCode != null && lastCode.CreatedAt > DateTime.UtcNow.AddSeconds(-60))
                throw new Exception("Too many requests");

            var code = GenerateCode();

            _db.EmailCodes.Add(new EmailCode
            {
                Email = email,
                Code = code,
                ExpireAt = DateTime.UtcNow.AddMinutes(10)
            });

            await _db.SaveChangesAsync();

            await _email.SendCode(email, code);
        }

        private string GenerateCode()
        {
            using var rng = new RNGCryptoServiceProvider();
            var bytes = new byte[4];
            rng.GetBytes(bytes);

            var num = BitConverter.ToUInt32(bytes, 0) % 900000 + 100000;
            return num.ToString();
        }
    }
}
