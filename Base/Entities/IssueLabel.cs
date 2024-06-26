using System.ComponentModel.DataAnnotations.Schema;

namespace Base.Entities;

[Table("issue_label", Schema = "it")]
public class IssueLabel
{
    public long Id { get; set; }
    public virtual Issue Issue { get; set; }
    public long IssueId { get; set; }
    public virtual Label Label { get; set; }
    public long LabelId { get; set; }
    public long RecDate { get; set; }
}