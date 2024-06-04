using Base.Entities;
using Base.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Base.Repo;

public class Uow : IUow
{
    public DbContext Context { get; }
    private readonly IServiceProvider _serviceProvider;

    public Uow(DbContext context, IServiceProvider serviceProvider)
    {
        Context = context;
        _serviceProvider = serviceProvider;
    }

    public void Commit() => Context.SaveChanges();

    public void Detach(BaseEntity entity)
    {
        Context.Entry(entity).State = EntityState.Detached;
    }
        
    public void DetachInBulk(List<BaseEntity> list)
    {
        list.ForEach(Detach);
    }


    public async Task CommitAsync() => await Context.SaveChangesAsync();

    public T Repo<T>() => _serviceProvider.GetRequiredService<T>();

    public async Task CreateAsync<T>(T entity) => await Context.AddAsync(entity);
    public async Task CreateRangeAsync<T>(IEnumerable<T> list) where T : class => await Context.AddRangeAsync(list);

    public void Update<T>(T entity) => Context.Update(entity);

    public void UpdateRange<T>(IEnumerable<T> list) where T : class => Context.UpdateRange(list);

    public void Remove<T>(T entity) => Context.Remove(entity);

    public void RemoveRange<T>(IEnumerable<T> list) where T : class => Context.RemoveRange(list);
    public void ClearChangeTacker()
    {
        Context.ChangeTracker.Clear();
    }
}