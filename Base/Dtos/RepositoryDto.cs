namespace Base.Dtos;

public class RepositoryDto
{
    public long repository_id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Visibility { get; set; }
    public string Branch { get; set; }
    public long TotalIssuesCount { get; set; }
    public long TotalOpenIssuesCount { get; set; }
    public long TotalClosedIssuesCount { get; set; }
}