using Microsoft.AspNetCore.Mvc.Rendering;

namespace IT_Web.Areas.IT.VIewModels;

public class IssueCreateVm
{
    public string Title { get; set; }
    public string Description { get; set; }
    
    public List<long> LabelIds { get; set; }
    public SelectList LabelList { get; set; }
    public long RepositoryId { get; set; }
}