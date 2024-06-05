namespace Base.Entities;

public class Repository : BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Visibility { get; set; }
}