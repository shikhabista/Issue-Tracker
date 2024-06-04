using Base.Entities;
using Microsoft.EntityFrameworkCore;

namespace Base.Configuration;

public static class EntityRegisterer
{
    public static ModelBuilder AddBase(this ModelBuilder builder)
    {
        builder.Entity<User>();
        builder.Entity<Branch>();
        builder.Entity<Organization>();
        return builder;
    }
}