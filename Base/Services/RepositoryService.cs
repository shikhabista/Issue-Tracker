﻿using System.Transactions;
using Base.Dtos;
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
            "INSERT INTO it.repository (name, description, visibility, rec_by_id, rec_date, status, branch) VALUES (@Name, @Description, @Visibility, @RecById, @RecDate, @Status, @Branch)",
            repo);
        tx.Complete();
    }

    public async Task<Repository> GetRepository(long id)
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var repository = await _dbService.GetAsync<Repository>("SELECT * FROM it.repository where id=@id", new { id });
        tx.Complete();
        return repository;
    }

    public async Task<List<RepositoryDto>> GetRepositoryList()
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var repositoryList = await _dbService.GetAll<RepositoryDto>("SELECT * FROM it.repository", new { });
        tx.Complete();
        return repositoryList;
    }

    public async Task<Repository> UpdateRepository(Repository repository)
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        await _dbService.EditData(
            "Update it.repository SET name=@Name, description=@Description, visibility=@Visibility WHERE id=@Id",
            repository);
        tx.Complete();
        return repository;
    }

    public async Task<bool> DeleteRepository(long id)
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        await _dbService.EditData("DELETE FROM it.repository WHERE id=@Id", new {id});
        tx.Complete();
        return true;
    }
}