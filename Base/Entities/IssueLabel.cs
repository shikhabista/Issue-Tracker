namespace Base.Entities;

public class IssueLabel : BaseEntity
{
    public virtual Issue Issue { get; set; }
    public long IssueId { get; set; }
    public virtual Label Label { get; set; }
    public long LabelId { get; set; }
}