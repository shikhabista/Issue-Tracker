using Base.Entities;
using Microsoft.EntityFrameworkCore;

namespace Base.Repo.Interfaces;

public interface IUow
{
    DbContext Context { get; }
    void Commit();

    Task CommitAsync();
    void Detach(BaseEntity entity);
    void DetachInBulk(List<BaseEntity> list);

    T Repo<T>();
    Task CreateAsync<T>(T entity);
    Task CreateRangeAsync<T>(IEnumerable<T> list) where T : class;
        
    void Update<T>(T entity);
    void UpdateRange<T>(IEnumerable<T> list) where T : class;
    void Remove<T>(T entity);
    void RemoveRange<T>(IEnumerable<T> list) where T : class;

    void ClearChangeTacker();
}