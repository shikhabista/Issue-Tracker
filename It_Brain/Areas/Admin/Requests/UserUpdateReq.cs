namespace IT_Web.Areas.Admin.Requests;

public class UserUpdateReq
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string ContactNo { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
}