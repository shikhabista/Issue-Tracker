using System.ComponentModel.DataAnnotations.Schema;
using Base.Entities.Interfaces;

namespace Base.Entities;

[Table("user", Schema = "Base")]
public class User : IBaseEntity
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string NormalizedUserName { get; set; }
    public string NormalizedEmail { get; set; }
    public string ContactNo { get; set; }
    public string? Address { get; set; }
    public string PasswordHash { get; set; }
    public string SecurityStamp { get; set; }
    public virtual Branch Branch { get; set; }
    public long BranchId { get; set; }
}