namespace IT_Web.Areas.Admin.Requests;

public class PasswordUpdateReq
{
    public long Id { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }
}