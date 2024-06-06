using System.Transactions;
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
        await _dbService.EditData(
            "INSERT INTO base.repository (name, code) VALUES (@Name, @Code)",
            label);
        tx.Complete();
    }

    public async Task<Label> GetLabel(long id)
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var label = await _dbService.GetAsync<Label>("SELECT * FROM base.label where id=@id", new { id });
        tx.Complete();
        return label;
    }

    public async Task<List<Label>> GetLabelList()
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var labelList = await _dbService.GetAll<Label>("SELECT * FROM base.label", new { });
        tx.Complete();
        return labelList;
    }

    public async Task<Label> UpdateLabel(Label label)
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        await _dbService.EditData(
            "Update base.label SET name=@Name, code=@Code WHERE id=@Id",
            label);
        tx.Complete();
        return label;
    }

    public async Task<bool> DeleteLabel(long id)
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        await _dbService.EditData("DELETE FROM base.label WHERE id=@Id", new {id});
        tx.Complete();
        return true;
    }
}