using Base.Enums;

namespace Base.Entities;

public class Issue : BaseEntity
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public virtual List<Label> Labels { get; set; }
    public long LabelId { get; set; }
    public virtual User Assignee { get; set; }
    public long AssigneeId { get; set; }
    public IssueStatusEnum IssueStatus { get; set; }
    public DateTime Date { get; set; }
}