using System.ComponentModel.DataAnnotations.Schema;
using Base.Enums;

namespace Base.Entities;

[Table("issue", Schema = "it")]
public class Issue
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public virtual User? Assignee { get; set; }
    public long? AssigneeId { get; set; }
    public IssueStatusEnum IssueStatus { get; set; }
    public DateTime Date { get; set; }
    public virtual Repository Repository { get; set; }
    public long RepositoryId { get; set; }
    public DateTime? LastUpdated { get; set; }
}