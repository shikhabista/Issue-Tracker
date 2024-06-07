using Base.Entities;

namespace Base.Services.Interfaces;

public interface IIssueLabelService
{
    Task AddIssueLabel(IssueLabel issueLabel);
    Task<List<IssueLabel>> GetIssueLabelOf(long issueId);
    Task<IssueLabel> GetIssueLabel(long issueId, long labelId);
    Task<List<IssueLabel>> GetIssueLabelList();
    Task<IssueLabel> UpdateIssueLabel(IssueLabel issueLabel);
    Task<bool> RemoveIssueLabel(long id);
}