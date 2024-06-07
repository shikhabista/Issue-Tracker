using System.Transactions;
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
        await _dbService.EditData(
            "INSERT INTO base.issue_label (issue_id, label_id) VALUES (@issueId, @labelId)",
            issueLabel);
        tx.Complete();
    }

    public async Task<List<IssueLabel>> GetIssueLabelOf(long issueId)
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var issue = await _dbService.GetAll<IssueLabel>("SELECT * FROM base.issue_label where id=@issue_id", new { issueId });
        tx.Complete();
        return issue;
    }

    public async Task<IssueLabel> GetIssueLabel(long issueId, long labelId)
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var issue = await _dbService.GetAsync<IssueLabel>("SELECT * FROM base.issue_label where issueId=@issueId and labelId = @labelId",
            new { issueId, labelId });
        tx.Complete();
        return issue;
    }

    public async Task<List<IssueLabel>> GetIssueLabelList()
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var issueLabelList = await _dbService.GetAll<IssueLabel>("SELECT * FROM base.issue_label", new { });
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
        await _dbService.EditData("DELETE FROM base.issue WHERE id=@Id", new { id });
        tx.Complete();
        return true;
    }
}