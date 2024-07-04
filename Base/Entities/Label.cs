using System.ComponentModel.DataAnnotations.Schema;

namespace Base.Entities;

[Table("label", Schema = "it")]
public class Label 
{
    public long LabelId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Code { get; set; }
    public StatusEnum Status { get; set; } = StatusEnum.Active;
    public DateTime RecDate { get; set; }
}