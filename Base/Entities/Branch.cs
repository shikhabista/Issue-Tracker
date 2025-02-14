﻿using System.ComponentModel.DataAnnotations.Schema;
using Base.Entities.Interfaces;

namespace Base.Entities;

[Table("branch", Schema = "base")]
public class Branch : BranchMiniInfo, IBaseEntity
{
    public BranchMiniInfo ToMiniInfo => new BranchMiniInfo
    {
        Id = Id,
        Name = Name,
        Code = Code,
        Address = Address,
        ContactNo = ContactNo,
        Email = Email,
        Status = Status
    };
}

public class BranchMiniInfo
{
    public long Id { get; set; }
    public string Name { get; set; }
    public long Code { get; set; }
    public string Address { get; set; }
    public string ContactNo { get; set; }
    public string? Email { get; set; }
    public StatusEnum Status { get; set; }
}