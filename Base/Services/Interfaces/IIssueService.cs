using Base.Entities;

namespace Base.Services.Interfaces;

public interface IIssueService
{
    Task CreateIssue(Issue issue);
    Task<Issue> GetIssue(long id);
    Task<List<Issue>> GetIssueList();
    Task<Issue> UpdateIssue(Issue issue);
    Task<bool> DeleteIssue(long id);
}