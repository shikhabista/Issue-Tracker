using System.Transactions;
using Base.Entities;
using Base.Services.Interfaces;

namespace Base.Services;

public class RepositoryService : IRepositoryService
{
    private readonly IDbService _dbService;

    public RepositoryService(IDbService dbService)
    {
        _dbService = dbService;
    }

    public async Task CreateRepository(Repository repo)
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        await _dbService.EditData(
            "INSERT INTO base.repository (name, description, visibility) VALUES (@Name, @Description, @Visibility)",
            repo);
        tx.Complete();
    }

    public async Task<Repository> GetRepository(long id)
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var repository = await _dbService.GetAsync<Repository>("SELECT * FROM base.repository where id=@id", new { id });
        tx.Complete();
        return repository;
    }

    public async Task<List<Repository>> GetRepositoryList()
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var repositoryList = await _dbService.GetAll<Repository>("SELECT * FROM base.repository", new { });
        tx.Complete();
        return repositoryList;
    }

    public async Task<Repository> UpdateRepository(Repository repository)
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        await _dbService.EditData(
            "Update base.repository SET name=@Name, description=@Description, visibility=@Visibility WHERE id=@Id",
            repository);
        tx.Complete();
        return repository;
    }

    public async Task<bool> DeleteRepository(long id)
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        await _dbService.EditData("DELETE FROM base.repository WHERE id=@Id", new {id});
        tx.Complete();
        return true;
    }
}