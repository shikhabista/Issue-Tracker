namespace Base.Dtos.IT;

public class IssueDto
{
    public long id { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public long assignee_id { get; set; }
    public long repository_id { get; set; }
    public DateTime date { get; set; }
    public long issue_status { get; set; }
    public DateTime LastUpdated { get; set; }
}