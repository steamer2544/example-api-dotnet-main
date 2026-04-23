using myapi.DTOs.Requests;
using myapi.DTOs.Responses;

namespace myapi.Services.Interfaces
{
    public interface IAuthService
    {
        Task<UserResponse?> Register(RegisterRequest req);
        Task<AuthResponse?> Login(LoginRequest req);
    }
}
