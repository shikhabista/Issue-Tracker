using Base.Dtos.IT;

namespace IT_Web.Areas.IT.VIewModels;

public class IssueListVm
{
    public string Repository { get; set; }
    public List<IssueDto> IssueList { get; set; }
}