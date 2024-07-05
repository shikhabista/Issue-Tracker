using System.Security.Claims;
using Base.Helpers;
using Base.Repo.Interfaces;
using Base.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Base.Services;

public class AuthService : IAuthService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserRepo _userRepository;

    public AuthService(IHttpContextAccessor httpContextAccessor, IUserRepo userRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _userRepository = userRepository;
    }

    public async Task Login(string username, string password)
    {
        var user = await _userRepository.FindSingleOrThrowAsync(x => x.Email.ToLower() == username.ToLower().Trim());
        if (user == null)
        {
            throw new Exception("User not found");
        }

        if (!Crypter.IsMatching(password, user.PasswordHash))
        {
            throw new Exception("Invalid password");
        }

        var httpContext = _httpContextAccessor.HttpContext;
        var claims = new List<Claim>
        {
            new("Id", user.Id.ToString()),
            new("Name", user.UserName),
        };
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity));
    }

    public async Task LogOut()
    {
        if (_httpContextAccessor.HttpContext == null) return;
        await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}