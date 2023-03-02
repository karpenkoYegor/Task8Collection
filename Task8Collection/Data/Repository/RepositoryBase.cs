using Task8Collection.Data.Repository.Intefaces;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Task8Collection.Data.Repository;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected ApplicationDbContext Context { get; set; }
    public RepositoryBase(ApplicationDbContext context)
    {
        Context = context;
    }

    public T FindByTableElement(Expression<Func<T, bool>> expression) =>
        Context.Set<T>().Where(expression).AsNoTracking().FirstOrDefault();

    public IQueryable<T> GetElementByTableId(Expression<Func<T, bool>> expression) =>
        Context.Set<T>().Where(expression).AsNoTracking();

    public void Create(T entity) => Context.Set<T>().Add(entity);
    public void CreateAll(IEnumerable<T> entities)
    {
        Context.Set<T>().AddRange(entities);
    }

    public int ChangeField(Expression<Func<T, bool>> expression, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setPropertyCall)
    {
        return Context.Set<T>().Where(expression)
            .ExecuteUpdate(setPropertyCall);
    }
    public void Update(T entity) => Context.Set<T>().Update(entity);

    public void Delete(T entity) => Context.Set<T>().Remove(entity);
    public IQueryable<T> GetAll() => Context.Set<T>();
}