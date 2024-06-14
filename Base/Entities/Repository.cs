namespace Base.Entities;

public class Repository : BaseEntity
{
    public const string Private = "Private";
    public const string Public = "Public";
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Visibility { get; set; }
}