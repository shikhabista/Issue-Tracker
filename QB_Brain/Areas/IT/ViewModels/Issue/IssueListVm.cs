using Base.Dtos.IT.Issue;

namespace IT_Web.Areas.IT.VIewModels.Issue;

public class IssueListVm
{
    public string Repository { get; set; }
    public List<IssueDto> IssueList { get; set; }
}