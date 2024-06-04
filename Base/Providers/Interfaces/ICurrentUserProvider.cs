using Base.Entities;

namespace Base.Providers.Interfaces;

public interface ICurrentUserProvider
{
    long GetUserId();
    Task<User> GetCurrentUser();
    Task<long> GetUserBranchId();
}