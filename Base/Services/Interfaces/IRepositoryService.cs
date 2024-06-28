using Base.Dtos;
using Base.Entities;

namespace Base.Services.Interfaces;

public interface IRepositoryService
{
    Task CreateRepository(Repository repository);
    Task<Repository> GetRepository(long id);
    Task<List<RepositoryDto>> GetRepositoryList();
    Task<Repository> UpdateRepository(Repository repository);
    Task<bool> DeleteRepository(long id);
}