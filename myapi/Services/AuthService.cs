using Microsoft.EntityFrameworkCore;
using myapi.Auth;
using myapi.Data;
using myapi.DTOs.Requests;
using myapi.DTOs.Responses;
using myapi.Helpers;
using myapi.Models;
using myapi.Services.Interfaces;

namespace myapi.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly JwtToken _jwt;

        public AuthService(AppDbContext context, JwtToken jwt)
        {
            _context = context;
            _jwt = jwt;
        }

        public async Task<UserResponse?> Register(RegisterRequest req)
        {
            var exists = await _context.Users
                .AsNoTracking()
                .AnyAsync(x => x.Username == req.Username);

            if (exists) return null;

            var user = new User
            {
                Username = req.Username,
                PasswordHash = PasswordHelper.Hash(req.Password),
                Name = req.Name,
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return new UserResponse
            {
                Id = user.Id,
                Username = user.Username,
            };
        }

        public async Task<AuthResponse?> Login(LoginRequest req)
        {
            var user = await _context.Users
                .AsNoTracking()
                .Where(x => x.Username == req.Username)
                .Select(x => new { x.Id, x.Username, x.PasswordHash, x.Name })
                .FirstOrDefaultAsync();

            if (user == null) return null;

            if (!PasswordHelper.Verify(req.Password, user.PasswordHash))
                return null;

            var token = _jwt.GenerateToken(new User
            {
                Id = user.Id,
                Username = user.Username,
            });

            return new AuthResponse
            {
                Id = user.Id,
                Username = user.Username,
                Name = user.Name,
                AccessToken = token,
                ExpireAt = DateTime.UtcNow.AddMinutes(60)
            };
        }
    }
}
