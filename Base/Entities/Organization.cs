using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Base.Entities;

[Table("organization_info", Schema = "Base")]
public class Organization
{
    public const string FileDir = "Organization";

    protected Organization(){}
    public Organization(string itemKey, string itemValue)
    {
        ItemKey = itemKey;
        ItemValue = itemValue;
    }
    [Key]
    public string ItemKey { get; set; }
    public string ItemValue { get; set; }
}

public class OrganizationKeyConst
{
    public const string Name = "Name";
    public const string Address = "Address";
    public const string Email = "Email";
    public const string PhoneNo = "PhoneNo";
    public const string Website = "Website";
    public const string Logo = "Logo";
}