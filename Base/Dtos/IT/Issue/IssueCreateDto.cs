using Base.Enums;

namespace Base.Dtos.IT.Issue;

public class IssueCreateDto
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public long? AssigneeId { get; set; }
    public IssueStatusEnum IssueStatus { get; set; }
    public DateTime Date { get; set; }
    public long RepositoryId { get; set; }
    public DateTime? LastUpdated { get; set; }
    public List<long>? LabelIds { get; set; }
}