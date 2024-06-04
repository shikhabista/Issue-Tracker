using Base.Configuration;
using Base.Entities;
using Base.Entities.Interfaces;
using Base.MigrationHistoryOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QB_Web;

public class AppDbContext(IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder
            .UseNpgsql(configuration.GetConnectionString("Default"))
            .ReplaceService<IHistoryRepository, MigrationHistory>();
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.AddBase();
        // modelBuilder.RegisterQb();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        BeforeSaveChanges();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void BeforeSaveChanges()
    {
        ChangeTracker.DetectChanges();
        var entries = ChangeTracker.Entries()
            .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Deleted).ToList();
        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    if (entry.Entity is BaseEntity entity)
                    {
                        entity.RecDate = DateTime.UtcNow;
                        if (entity.RecById == 0 && entity.RecBranchId == 0)
                        {
                            var id = httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(x => x.Type == "Id");
                            var user = GetUser(Convert.ToInt64(id.Value));
                            if (user != null)
                            {
                                entity.RecBy = user;
                                entity.RecBranchId = user.BranchId;
                            }
                        }
                    }

                    break;

                case EntityState.Deleted:
                    if (entry.Entity is ISoftDelete delEntity)
                    {
                        entry.State = EntityState.Modified;
                        delEntity.RecStatus = 'D';
                    }
                    else
                    {
                        entry.State = EntityState.Deleted;
                    }

                    break;
            }
        }
    }

    User? GetUser(long id)
    {
        object user;
        if (!httpContextAccessor.HttpContext.Items.TryGetValue("_current_logged_in_user", out user))
        {
            var fromDb = Set<User>().FirstOrDefault(x => x.Id == id);
            httpContextAccessor.HttpContext.Items["_current_logged_in_user"] = fromDb;
            return fromDb;
        }

        return user == null ? null : user as User;
    }
}