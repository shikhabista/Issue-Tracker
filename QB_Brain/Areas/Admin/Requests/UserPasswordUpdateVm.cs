namespace QB_Web.Areas.v1.Admin.Requests;

public class UserPasswordUpdateVm
{
    public long Id { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}