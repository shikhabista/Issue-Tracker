namespace Base.Services.Interfaces;

public interface IAuthService
{
    Task Login(string username, string password);
}