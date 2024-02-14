using System.Linq.Expressions;

namespace ApiCatalogo.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();

        T? Get(Expression<Func<T, bool>> predicate); // aceita como argumento uma expressão lambda

        T Create(T entity);
        
        T Update(T entity);

        T Delete(T entity);
    }
}
