namespace Base.Services.Interfaces;

public interface IDbService
{
    Task<T> GetAsync<T>(string command, object @params);
    Task<List<T>> GetAll<T>(string command, object @params);
    Task<int> EditData(string command, object @params);
}