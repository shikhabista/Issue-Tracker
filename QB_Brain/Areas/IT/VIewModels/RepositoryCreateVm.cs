namespace IT_Web.Areas.IT.VIewModels;

public class RepositoryCreateVm
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? IsPublic { get; set; }
    public string? IsPrivate { get; set; }
}