using Base.Dtos.IT;

namespace IT_Web.Areas.IT.VIewModels;

public class LabelCreateVm
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string? Description { get; set; }

    public List<LabelDto> Labels { get; set; }
}