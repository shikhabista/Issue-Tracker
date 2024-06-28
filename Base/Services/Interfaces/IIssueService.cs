using Base.Dtos.IT;
using Base.Entities;

namespace Base.Services.Interfaces;

public interface IIssueService
{
    Task CreateIssue(Issue issue);
    Task<IssueDto> GetIssue(long id);
    Task<List<IssueDto>> GetIssueList();
    Task<Issue> UpdateIssue(Issue issue);
    Task<bool> DeleteIssue(long id);
    Task<List<IssueDto>> GetIssuesOf(long repoId);
}