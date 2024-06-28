using System.Data;
using System.Transactions;
using Base.Dtos.IT;
using Base.Entities;
using Base.Enums;
using Base.Repo.Interfaces;
using Base.Services.Interfaces;
using Dapper;
using Npgsql;

namespace Base.Services;

public class IssueService : IIssueService
{
    private readonly IDbService _dbService;
    private IUserRepo _userRepo;
    private readonly IDbConnection _db;


    public IssueService(IDbService dbService, IUserRepo userRepo, IDbConnection db)
    {
        _dbService = dbService;
        _userRepo = userRepo;
        _db = db;
    }

    public async Task CreateIssue(Issue issue)
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        await _dbService.EditData(
            "INSERT INTO it.issue (title, description, issueStatus, date,repository_id, assignee_id, last_updated) " +
            "VALUES (@Name, @Description, @IssueStatus, @Date,@RepositoryId, @AssigneeId, @LastUpdated)",
            issue);
        tx.Complete();
    }

    public async Task<IssueDto> GetIssue(long id)
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var issue = await _dbService.GetAsync<IssueDto>("SELECT * FROM it.issue where id=@id", new { id });
        tx.Complete();
        return issue;
    }

    public async Task<List<IssueDto>> GetIssueList()
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var issueList = await _dbService.GetAll<IssueDto>("SELECT * FROM it.issue", new { });
        tx.Complete();
        return issueList;
    }

    public async Task<Issue> UpdateIssue(Issue issue)
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        await _dbService.EditData(
            "Update it.issue SET title=@Title, description=@Description, issueStatus=@IssueStatus, date=@Date, " +
            "assigned_id=@AssignedId, repository_id=@RepositoryId, last_updated = @LastUpdated  WHERE id=@Id",
            issue);
        tx.Complete();
        return issue;
    }

    public async Task<bool> DeleteIssue(long id)
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        await _dbService.EditData("DELETE FROM it.issue WHERE id=@Id", new { id });
        tx.Complete();
        return true;
    }

    public async Task<List<IssueDto>> GetIssuesOf(long repositoryId)
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var query = $"select * from it.issue where repository_id = @repositoryId;";
        var list = (await _dbService.GetAll<IssueDto>(query, new
        {
            repositoryId
        })).ToList();

        return list;
       
    }
}