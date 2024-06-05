using Base.Dtos;
using Base.Entities;

namespace Base.Services.Interfaces;

public interface IRepositoryService
{
    Task CreateRepository(Repository repository);
    Task<Repository> GetRepository(long id);
    Task<List<Repository>> GetRepositoryList();
    Task<Repository> UpdateRepository(Repository repository);
    Task<bool> DeleteRepository(long id);
}