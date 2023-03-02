using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Task8Collection.Data.Repository.Intefaces;

public interface IRepositoryBase<T>
{
    public T FindByTableElement(Expression<Func<T, bool>> expression);
    public IQueryable<T> GetElementByTableId(Expression<Func<T, bool>> expression);
    int ChangeField(Expression<Func<T, bool>> expression, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setPropertyCall);
    void Create(T entity);
    void CreateAll(IEnumerable<T> entities);
    void Update(T entity);
    void Delete(T entity);
    IQueryable<T> GetAll();
}