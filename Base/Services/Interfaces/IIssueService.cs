using Base.Dtos.IT;
using Base.Dtos.IT.Issue;
using Base.Entities;

namespace Base.Services.Interfaces;

public interface IIssueService
{
    Task CreateIssue(IssueCreateDto issue);
    Task<IssueDto> GetIssue(long id);
    Task<List<IssueDto>> GetIssueList();
    Task<Issue> UpdateIssue(Issue issue);
    Task<bool> DeleteIssue(long id);
    Task<(List<IssueDto> list, string repoName)> GetIssuesOf(long repoId);
}