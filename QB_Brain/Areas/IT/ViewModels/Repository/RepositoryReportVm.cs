using Base.Dtos;

namespace IT_Web.Areas.IT.ViewModels.Repository;

public class RepositoryReportVm
{
    public long RepositoryId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Visibility { get; set; }
    public string Branch { get; set; }
    public long TotalIssuesCount { get; set; }
    public long TotalOpenIssuesCount { get; set; }
    public long TotalClosedIssuesCount { get; set; }
    public List<RepositoryDto> RepositoryList { get; set; }
}