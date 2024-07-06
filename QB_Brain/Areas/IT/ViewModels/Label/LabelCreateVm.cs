using System.ComponentModel.DataAnnotations;

namespace IT_Web.Areas.IT.VIewModels.Label;

public class LabelCreateVm
{
    [Required(ErrorMessage = "Label name is required")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Label Code is required")]

    public string Code { get; set; }
    public string? Description { get; set; }

    public List<Base.Entities.Label> Labels { get; set; }
}