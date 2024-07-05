namespace Base.Services.Interfaces;

public interface IDbService
{
    Task<T> GetAsync<T>(string command, object @params);
    Task<List<T>> GetAll<T>(string command, object @params);
    Task<int> Create(string command, object @params);
    Task<T> CreateAndReturn<T>(string command, object @params);
}