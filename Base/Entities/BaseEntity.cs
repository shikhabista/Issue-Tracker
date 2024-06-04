using System.ComponentModel.DataAnnotations.Schema;
using Base.Entities.Interfaces;

namespace Base.Entities;

public class BaseEntity : IBaseEntity
{
    public long Id { get; set; }
    public StatusEnum Status { get; set; } = StatusEnum.Active;
    public DateTime RecDate { get; set; }
    public virtual User RecBy { get; set; }
    public long RecById { get; set; }
    [NotMapped]public virtual Branch RecBranch { get; set; }
    [NotMapped]public long RecBranchId { get; set; }
    public long SourceId { get; set; }
    public char RecStatus { get; set; } = 'A';
}

public enum StatusEnum
{
    Active = 1,
    Inactive = 2
}