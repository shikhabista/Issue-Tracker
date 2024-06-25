using Microsoft.AspNetCore.Mvc.Rendering;

namespace IT_Web.Areas.IT.VIewModels;

public class IssueCreateVm
{
    public string Title { get; set; }
    public string Description { get; set; }
    
    public long LabelId { get; set; }
    public SelectList LabelList { get; set; }
}

public class Labels
{
    public string Id { get; set; }
    public string Name { get; set; }
}