namespace Base.Dtos;

public class UserAddDto
{
    public string Name { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string ContactNo { get; set; }
    public string? Address { get; set; }
    public long BranchId { get; set; }
}