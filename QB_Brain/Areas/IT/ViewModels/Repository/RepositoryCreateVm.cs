using System.ComponentModel.DataAnnotations;

namespace IT_Web.Areas.IT.ViewModels.Repository;

public class RepositoryCreateVm
{
    [Required(ErrorMessage = "Repository Name is required")]
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? IsPublic { get; set; }
    public string? IsPrivate { get; set; }
}