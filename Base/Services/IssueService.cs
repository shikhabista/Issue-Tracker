using System.Transactions;
using Base.Entities;
using Base.Repo.Interfaces;
using Base.Services.Interfaces;

namespace Base.Services;

public class IssueService : IIssueService
{
    private readonly IDbService _dbService;
    private IUserRepo _userRepo;

    public IssueService(IDbService dbService, IUserRepo userRepo)
    {
        _dbService = dbService;
        _userRepo = userRepo;
    }

    public async Task CreateIssue(Issue issue)
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        await _dbService.EditData(
            "INSERT INTO base.issue (title, description, issueStatus, date, assigneeId) VALUES (@Name, @Description, @IssueStatus, @Date, @AssigneeId)",
            issue);
        tx.Complete();
    }

    public async Task<Issue> GetIssue(long id)
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var issue = await _dbService.GetAsync<Issue>("SELECT * FROM base.issue where id=@id", new { id });
        tx.Complete();
        return issue;
    }

    public async Task<List<Issue>> GetIssueList()
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var issueList = await _dbService.GetAll<Issue>("SELECT * FROM base.issue", new { });
        tx.Complete();
        return issueList;
    }

    public async Task<Issue> UpdateIssue(Issue issue)
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        await _dbService.EditData(
            "Update base.issue SET title=@Title, description=@Description, issueStatus=@IssueStatus, date=@Date, assignedId=@AssignedId WHERE id=@Id",
            issue);
        tx.Complete();
        return issue;
    }

    public async Task<bool> DeleteIssue(long id)
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        await _dbService.EditData("DELETE FROM base.issue WHERE id=@Id", new { id });
        tx.Complete();
        return true;
    }
}