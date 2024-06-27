using System.ComponentModel.DataAnnotations.Schema;

namespace Base.Entities;

[Table("repository", Schema = "it")]
public class Repository
{
    public const string Private = "Private";
    public const string Public = "Public";

    public long Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Visibility { get; set; }
    public StatusEnum Status { get; set; } = StatusEnum.Active;
    public virtual User? RecBy { get; set; }
    public long? RecById { get; set; }
    public DateTime RecDate { get; set; }
}