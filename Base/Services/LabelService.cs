﻿using System.Transactions;
using Base.Dtos.IT;
using Base.Entities;
using Base.Services.Interfaces;

namespace Base.Services;

public class LabelService : ILabelService
{
    private readonly IDbService _dbService;

    public LabelService(IDbService dbService)
    {
        _dbService = dbService;
    }

    public async Task CreateLabel(Label label)
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        await _dbService.ExecuteQuery(
            "INSERT INTO it.label (name,description, code, status, rec_date) " +
            "VALUES (@Name, @Description, @Code, @Status, @RecDate)",
            label);
        tx.Complete();
    }

    public async Task<Label> GetLabel(long id)
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var label = await _dbService.GetAsync<Label>("SELECT * FROM it.label where id=@id", new { id });
        tx.Complete();
        return label;
    }

    public async Task<List<Label>> GetLabelList()
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var labelList = await _dbService.GetAll<Label>("SELECT * FROM it.label", new { });
        tx.Complete();
        return labelList;
    }

    public async Task<Label> UpdateLabel(Label label)
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        await _dbService.ExecuteQuery(
            "Update it.label SET name=@Name, description=@Description, code=@Code WHERE id=@Id",
            label);
        tx.Complete();
        return label;
    }

    public async Task<bool> DeleteLabel(long id)
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        await _dbService.ExecuteQuery("DELETE FROM it.label WHERE id=@id", new { id });
        tx.Complete();
        return true;
    }

    public async Task<bool> CheckIfDuplicateName(string code)
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var label = await _dbService.GetAsync<Label>($"SELECT * FROM it.label where lower(code) like '{@code.ToLower()}'", new { code });
        tx.Complete();
        bool isDuplicate = label != null;
        return isDuplicate;
    }
}