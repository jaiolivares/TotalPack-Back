using System.Linq.Expressions;

namespace Ttp.Arquitectura.Users.Domain.Interfaces.Repository
{
    public interface IGenericRepository<TEntity>
    {
        void Delete(object id);

        void Delete(TEntity entityToDelete);

        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");

        IEnumerable<TEntity> GetPage(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");

        TEntity GetByID(Guid id);

        void Insert(TEntity entity);

        void Update(TEntity entityToUpdate);

        void Save();
    }
}