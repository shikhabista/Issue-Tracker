using System.ComponentModel.DataAnnotations;

namespace IT_Web.Areas.IT.VIewModels.Repository;

public class RepositoryEditVm
{
    public long Id { get; set; }
    
    [Required(ErrorMessage = "Repository name is required")]
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Visibility { get; set; }
    public string? Branch { get; set; }
    public bool? IsPublic { get; set; }
    public bool? IsPrivate { get; set; }
    public DateTime? RecDate { get; set; }
}