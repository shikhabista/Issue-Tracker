using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Base.Repo.Interfaces;

public interface IGenericRepo<T> where T : class
{
    DbContext context { get; }
    public Task CreateAsync(T t);
    public void Update(T t);
    public void Remove(T t);
    public Task<List<T>> FindAllAsync();

    public Task<List<T>> FindByAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, 
        IQueryable<T>> callback = null);

    List<T> FindBy(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>,
        IQueryable<T>> callback = null);
    
    public Task<T> FindSingleAsync(Expression<Func<T, bool>> predicate);
    public Task<T> FindSingleOrThrowAsync(Expression<Func<T, bool>> predicate);
    public bool CheckIfExist(Expression<Func<T, bool>> predicate);
    public Task<bool> CheckIfExistAsync(Expression<Func<T, bool>> predicate);
    public Task<T> FindByIdAsync(long id);
    public T FindById(long id);
    public T FindOrThrow(long id);
    Task<T> FindOrThrowAsync(long id);
    public Task CreateRangeAsync(List<T> ts);
    public void UpdateRange(List<T> ts);
    public void RemoveRange(List<T> ts);
    public IQueryable<T> GetQueryable();

    public long Count(Expression<Func<T,bool>> predicate = null);
    public ValueTask<long> CountAsync(Expression<Func<T,bool>> predicate = null);
}