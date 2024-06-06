namespace IT_Web.Areas.IT.VIewModels;

public class IssueCreateVm
{
    public string Title { get; set; }
    public string Description { get; set; }
    public List<long> LabelId { get; set; } 
}