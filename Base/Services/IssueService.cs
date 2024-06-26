using System.Transactions;
using Base.Dtos.IT;
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
            "INSERT INTO public.issue (title, description, issueStatus, date,repository_id, assignee_id) VALUES (@Name, @Description, @IssueStatus, @Date,@RepositoryId, @AssigneeId)",
            issue);
        tx.Complete();
    }

    public async Task<IssueDto> GetIssue(long id)
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var issue = await _dbService.GetAsync<IssueDto>("SELECT * FROM base.issue where id=@id", new { id });
        tx.Complete();
        return issue;
    }

    public async Task<List<IssueDto>> GetIssueList()
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var issueList = await _dbService.GetAll<IssueDto>("SELECT * FROM public.issue", new { });
        tx.Complete();
        return issueList;
    }

    public async Task<Issue> UpdateIssue(Issue issue)
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        await _dbService.EditData(
            "Update base.issue SET title=@Title, description=@Description, issueStatus=@IssueStatus, date=@Date, assigned_id=@AssignedId WHERE id=@Id",
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