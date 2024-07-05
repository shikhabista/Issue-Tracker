using System.Transactions;
using Base.Dtos;
using Base.Dtos.IT.Issue;
using Base.Repo.Interfaces;
using Base.Services.Interfaces;

namespace Base.Services;

public class IssueService : IIssueService
{
    private readonly IDbService _dbService;
    private IUserRepo _userRepo;
    private readonly IIssueLabelService _issueLabelService;

    public IssueService(IDbService dbService, IUserRepo userRepo, IIssueLabelService issueLabelService)
    {
        _dbService = dbService;
        _userRepo = userRepo;
        _issueLabelService = issueLabelService;
    }

    public async Task<IssueDto> CreateIssue(IssueCreateDto issue)
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var query = "INSERT INTO it.issue (title, description, issue_status, date,repository_id, assignee_id, last_updated) " +
                    "VALUES (@Title, @Description, @IssueStatus, @Date,@RepositoryId, @AssigneeId, @LastUpdated)"
                    + "Returning *";
        var createdIssue = await _dbService.CreateAndReturn<IssueDto>(query, issue);
        tx.Complete();
        return createdIssue;
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

    public async Task<IssueDto> UpdateIssue(IssueDto dto)
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        await _dbService.ExecuteQuery(
            "Update it.issue SET title=@title, description=@description, issue_status=@isssue_status, date=@date, " +
            "assigned_id=@assigned_id, repository_id=@repository_id, last_updated = @last_updated  WHERE id=@Id",
            dto);
        tx.Complete();
        return dto;
    }

    public async Task<bool> CloseIssue(long id)
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        await _dbService.ExecuteQuery("Update it.issue Set issue_status = 2 WHERE id=@Id", new { id });
        tx.Complete();
        return true;
    }

    public async Task<bool> OpenIssue(long id)
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        await _dbService.ExecuteQuery("Update it.issue Set issue_status = 1 WHERE id=@Id", new { id });
        tx.Complete();
        return true;
    }

    public async Task<(List<IssueDto> list, string repoName)> GetIssuesOf(long repositoryId)
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var query = $"select * from it.issue where repository_id = @repositoryId;";
        var list = (await _dbService.GetAll<IssueDto>(query, new
        {
            repositoryId
        })).ToList();
        var repository = await _dbService.GetAsync<RepositoryDto>("SELECT * FROM it.repository where id=@repositoryId",
            new { repositoryId });
        var repoName = repository.Name;
        
        foreach (var item in list)
        {
            var issueLabelQuery = $"select l.name from it.issue_label il join it.label l on il.label_id = l.id where issue_id = @id";
            var labelNames = await _dbService.GetAll<string>(issueLabelQuery, new {id = item.id});
            item.label_names = labelNames;
        }
        
        return (list, repoName);
    }
}