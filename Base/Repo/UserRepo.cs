using Base.Entities;
using Base.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Base.Repo;

public class UserRepo : GenericRepo<User>, IUserRepo
{
    public UserRepo(DbContext context) : base(context)
    {
    }
}