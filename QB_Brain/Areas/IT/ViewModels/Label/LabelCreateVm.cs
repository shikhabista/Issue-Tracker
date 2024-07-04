namespace IT_Web.Areas.IT.VIewModels.Label;

public class LabelCreateVm
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string? Description { get; set; }

    public List<Base.Entities.Label> Labels { get; set; }
}