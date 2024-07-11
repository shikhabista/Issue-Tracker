namespace IT_Web.Areas.Admin.Requests;

public class UserPasswordUpdateVm
{
    public long Id { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}