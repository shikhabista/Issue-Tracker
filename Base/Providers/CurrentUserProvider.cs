using Base.Entities;
using Base.Providers.Interfaces;
using Base.Repo.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Base.Providers;

public class CurrentUserProvider : ICurrentUserProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserRepo _userRepo;
    private static User? user = null;
    
    public CurrentUserProvider(IHttpContextAccessor httpContextAccessor, IUserRepo userRepo)
    {
        _httpContextAccessor = httpContextAccessor;
        _userRepo = userRepo;
    }

    public long GetUserId()
    {
        var id = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id");
        if (id == null) throw new Exception("User Id not found");
        return Convert.ToInt64(id.Value);
    }

    public async Task<User> GetCurrentUser()
    {
        var userId = GetUserId();
        return user ??= await _userRepo.FindOrThrowAsync(userId);
    }

    public async Task<long> GetUserBranchId()
    {
        var user = await GetCurrentUser();
        return user.BranchId;
    }
}