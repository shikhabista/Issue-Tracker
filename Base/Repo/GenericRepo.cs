using System.Linq.Expressions;
using Base.Entities;
using Base.Entities.Interfaces;
using Base.Exceptions;
using Base.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Base.Repo;

public class GenericRepo<T> : IGenericRepo<T> where T : class
{
        public DbContext context { get; }

        public GenericRepo(DbContext context) => this.context = context;

        public virtual async Task CreateAsync(T t) => await context.Set<T>().AddAsync(t);

        public virtual void Update(T t) => context.Set<T>().Update(t);

        public virtual void Remove(T t) => context.Set<T>().Remove(t);

        public virtual async Task<List<T>> FindAllAsync() => await GetQueryable().ToListAsync();

        public virtual async Task<List<T>> FindByAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>,
            IQueryable<T>> callback = null)
        {
            predicate ??= arg => true;
            var queryable = GetPredicatedQueryable(predicate);
            queryable = ApplyCallback(queryable, callback);
            return await queryable.ToListAsync();
        }

        public virtual List<T> FindBy(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>,
            IQueryable<T>> callback = null)
        {
            predicate ??= arg => true;
            var queryable = GetPredicatedQueryable(predicate);
            queryable = ApplyCallback(queryable, callback);
            return queryable.ToList();
        }

        public virtual async Task<T> FindSingleAsync(Expression<Func<T, bool>> predicate) =>
            (T)await GetQueryable().FirstOrDefaultAsync(predicate);


        public virtual bool CheckIfExist(Expression<Func<T, bool>> predicate) => GetQueryable().Any(predicate);

        public virtual Task<bool> CheckIfExistAsync(Expression<Func<T, bool>> predicate) =>
            GetPredicatedQueryable(predicate).AnyAsync();

        public virtual async Task<T> FindByIdAsync(long id)
        {
            return await GetQueryable()
                .FirstOrDefaultAsync(x => ((IBaseEntity)x).Id == id);
        }

        public virtual T FindById(long id) => GetQueryable().FirstOrDefault(x => ((IBaseEntity)x).Id == id);

        public virtual T FindOrThrow(long id)
        {
            var entity = FindById(id);
            if (entity == null) throw new EntityNotFoundException(typeof(T).Name);
            return entity;
        }

        public virtual async Task<T> FindOrThrowAsync(long id)
        {
            var entity = await FindByIdAsync(id);
            if (entity == null) throw new EntityNotFoundException(typeof(T).Name);
            return entity;
        }

        public virtual async Task<T> FindSingleOrThrowAsync(Expression<Func<T, bool>> predicate)
        {
            var entity = await FindSingleAsync(predicate);
            if (entity == null) throw new EntityNotFoundException(typeof(T).Name);
            return entity;
        }


        public virtual async Task CreateRangeAsync(List<T> ts) => await context.Set<T>().AddRangeAsync(ts);
        public virtual void UpdateRange(List<T> ts) => context.Set<T>().UpdateRange(ts);
        public virtual void RemoveRange(List<T> ts) => context.Set<T>().RemoveRange(ts);

        protected IQueryable<T> GetPredicatedQueryable(Expression<Func<T, bool>> expression) =>
            GetQueryable().Where(expression ?? (x => true));

        protected IQueryable<T> ApplyCallback(IQueryable<T> queryable,
            Func<IQueryable<T>, IQueryable<T>> callback = null) =>
            callback == null ? queryable : callback(queryable);

        public virtual IQueryable<T> GetQueryable() => context.Set<T>();

        public long Count(Expression<Func<T, bool>> predicate = null)
            => GetPredicatedQueryable(predicate).LongCount();

        public async ValueTask<long> CountAsync(Expression<Func<T, bool>> predicate = null)
            => await GetPredicatedQueryable(predicate).LongCountAsync();
    }

    public static class GenericRepoFilter
    {
        public static IQueryable<T> FilterActiveStatus<T>(this IQueryable<T> queryable) where T : BaseEntity
        {
            queryable = queryable.Where(x => x.Status == StatusEnum.Active);
            return queryable;
        }
    }
