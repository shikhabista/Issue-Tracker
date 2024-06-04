using Base.Entities;
using Base.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Base.Repo;

public class BranchRepo : GenericRepo<Branch>, IBranchRepo
{
    public BranchRepo(DbContext context) : base(context)
    {
    }
}