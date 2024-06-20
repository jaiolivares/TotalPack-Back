using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Ttp.Arquitectura.Users.Domain.Interfaces.Repository;

namespace Ttp.Arquitectura.Users.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected DbContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(UsersContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual IEnumerable<TEntity> GetPage(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public virtual TEntity GetByID(Guid id)
        {
            return dbSet.Find(id);
        }

        public virtual TEntity GetByID(int id)
        {
            return dbSet.Find(id);
        }

        public virtual IEnumerable<TEntity> GetByIdAdress(Guid idUser)
        {
            return dbSet.Where(x => EF.Property<Guid>(x, "IdUser") == idUser);
        }

        public virtual TEntity GetByPrincipal(Guid idUser)
        {
            return dbSet.FirstOrDefault(x => EF.Property<Guid>(x, "IdUser") == idUser && EF.Property<bool>(x, "Principal"));
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void UpdatePrincipal(int idAdress)
        {
            var entityToUpdate = dbSet.FirstOrDefault(x => EF.Property<int>(x, "IdAdress") == idAdress);
            if (entityToUpdate == null)
            {
                return;
                //throw new Exception($"Address with IdAdress {idAdress} not found");
            }

            // Obtener el IdUser del registro
            var idUserPropertyInfo = entityToUpdate.GetType().GetProperty("IdUser");
            if (idUserPropertyInfo == null || idUserPropertyInfo.PropertyType != typeof(Guid))
            {
                throw new Exception("The entity does not contain a property named 'IdUser' of type Guid.");
            }
            var idUser = (Guid)idUserPropertyInfo.GetValue(entityToUpdate);

            var entitiesToUpdate = dbSet.Where(e => EF.Property<Guid>(e, "IdUser") == idUser).ToList();

            foreach (var entity in entitiesToUpdate)
            {
                var principalPropertyInfo = entity.GetType().GetProperty("Principal");
                if (principalPropertyInfo != null && principalPropertyInfo.PropertyType == typeof(bool))
                {
                    principalPropertyInfo.SetValue(entity, false);
                }
            }

            var targetPrincipalPropertyInfo = entityToUpdate.GetType().GetProperty("Principal");
            if (targetPrincipalPropertyInfo != null && targetPrincipalPropertyInfo.PropertyType == typeof(bool))
            {
                targetPrincipalPropertyInfo.SetValue(entityToUpdate, true);
            }

            Save();
        }

        public virtual void Delete(Guid id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void DeleteAll(Guid id)
        {
            var entitiesToDelete = dbSet.Where(x => EF.Property<Guid>(x, "IdUser") == id).ToList();
            foreach (var entityToDelete in entitiesToDelete)
            {
                Delete(entityToDelete);
            }
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}