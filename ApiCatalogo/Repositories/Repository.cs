using ApiCatalogo.Context;
using ApiCatalogo.Interfaces;
using System.Linq.Expressions;

namespace ApiCatalogo.Repositories
{
    public class Repository<T>: IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        
        public Repository(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList(); // O método Set<T>() é usado para obter um conjunto (ou tabela) de entidades do tipo T no contexto. Ele retorna um objeto do tipo DbSet<T>, que representa a coleção de entidades associadas a uma tabela específica no banco de dados.
        }

        public T? Get(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        public T Create(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public T Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();

            return entity;
        }

        public T Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();

            return entity;
        }
    }
}
