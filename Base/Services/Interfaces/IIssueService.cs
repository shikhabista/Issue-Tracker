using Base.Dtos.IT.Issue;
using Base.Entities;

namespace Base.Services.Interfaces;

public interface IIssueService
{
    Task<IssueDto> CreateIssue(IssueCreateDto issue);
    Task<IssueDto> GetIssue(long id);
    Task<List<IssueDto>> GetIssueList();
    Task<IssueDto> UpdateIssue(IssueDto issueDto);
    Task<bool> CloseIssue(long id);
    Task<bool> OpenIssue(long id);
    Task<(List<IssueDto> list, string repoName)> GetIssuesOf(long repoId);
}