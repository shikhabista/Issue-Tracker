using System.Transactions;
using Base.Dtos.IT;
using Base.Entities;
using Base.Services.Interfaces;

namespace Base.Services;

public class IssueLabelService : IIssueLabelService
{
    private readonly IDbService _dbService;

    public IssueLabelService(IDbService dbService)
    {
        _dbService = dbService;
    }

    public async Task AddIssueLabel(IssueLabel issueLabel)
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        await _dbService.Create(
            "INSERT INTO it.issue_label (issue_id, label_id, rec_date) VALUES (@IssueId, @LabelId, @RecDate)",
            issueLabel);
        tx.Complete();
    }

    public async Task<List<IssueLabelDto>> GetIssueLabelOf(long issueId)
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var labels = await _dbService.GetAll<IssueLabelDto>("SELECT * FROM it.issue_label where id=@issueId", new { issueId });
        tx.Complete();
        return labels;
    }

    public async Task<List<long>> GetLabelIdsOf(long issueId)
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var labelIds = await _dbService.GetAll<long>("SELECT label_id FROM it.issue_label where issue_id=@issueId", new { issueId });
        tx.Complete();
        return labelIds;
    }

    public async Task<IssueLabel> GetIssueLabel(long issueId, long labelId)
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var issue = await _dbService.GetAsync<IssueLabel>("SELECT * FROM it.issue_label where issueId=@issueId and labelId = @labelId",
            new { issueId, labelId });
        tx.Complete();
        return issue;
    }

    public async Task<List<IssueLabel>> GetIssueLabelList()
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var issueLabelList = await _dbService.GetAll<IssueLabel>("SELECT * FROM it.issue_label", new { });
        tx.Complete();
        return issueLabelList;
    }

    public Task<IssueLabel> UpdateIssueLabel(IssueLabel issueLabel)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RemoveIssueLabel(long id)
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        await _dbService.Create("DELETE FROM it.issue WHERE id=@Id", new { id });
        tx.Complete();
        return true;
    }
}