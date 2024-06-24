namespace IT_Web.Areas.IT.VIewModels;

public class IssueCreateVm
{
    public string Title { get; set; }
    public string Description { get; set; }
    public List<Labels> LabelList { get; set; }
}

public class Labels
{
    public string Id { get; set; }
    public string Name { get; set; }
}