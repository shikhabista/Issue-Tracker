using Base.Entities;
using Base.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Base.Repo;

public class OrganizationRepo : GenericRepo<Organization>, IOrganizationRepo
{
    public OrganizationRepo(DbContext context) : base(context)
    {
    }
}