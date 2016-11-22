using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using LinqKit;
using CafeteriaApp.Data.Contexts;
using System.Threading.Tasks;

namespace CafeteriaApp.Data
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        internal AppDb Context;
        internal DbSet<TEntity> DbSet;

        public GenericRepository(AppDb context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
            context.Database.CommandTimeout = 180;
        }

        // The code Expression<Func<TEntity, bool>> filter means 
        //the caller will provide a lambda expression based on the TEntity type,
        //and this expression will return a Boolean value.
        public virtual IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.AsExpandable().Where(filter);
            }

            // applies the eager-loading expressions after parsing the comma-delimited list
            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return orderBy != null
                ? orderBy(query)
                : query;

        }

        public virtual IQueryable<T> Project<T>()
        {
            IQueryable<T> result = DbSet.Project().To<T>();
            return result;

        }


        public virtual async Task<ICollection<TResult>> GetAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            if (selector == null)
                throw new ArgumentNullException("selector");
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.AsExpandable().Where(filter);
            }

            // applies the eager-loading expressions after parsing the comma-delimited list
            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            // applies the eager-loading expressions after parsing the comma-delimited list


            return await (orderBy != null
                ? orderBy(query).Select(selector).ToListAsync()
                : query.Select(selector).ToListAsync());
        }

        public virtual TEntity GetByID(object id)
        {
            return DbSet.Find(id);
        }

        public virtual async Task<TEntity> GetByAsync(object id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual TEntity Insert(TEntity entity)
        {
            return DbSet.Add(entity);
        }


        public virtual void Delete(object id)
        {
            TEntity entityToDelete = DbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }
            DbSet.Remove(entityToDelete);
        }

        public virtual void Detach(TEntity entityToUpdate)
        {
            Context.Entry(entityToUpdate).State = EntityState.Detached;
        }
        public virtual void Update(TEntity entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void Update(TEntity entityToUpdate, List<string> excluded)
        {
            DbSet.Attach(entityToUpdate);
            var entry = Context.Entry(entityToUpdate);
            entry.State = EntityState.Modified;

            if (excluded != null)
            {
                foreach (var name in excluded)
                {
                    entry.Property(name).IsModified = false;
                }
            }
        }

        public virtual void DeleteRange(IQueryable<TEntity> entitiesToDelete)
        {
            foreach (var entity in entitiesToDelete)
            {
                if (Context.Entry(entity).State == EntityState.Detached)
                {
                    DbSet.Attach(entity);
                }
            }
            DbSet.RemoveRange(entitiesToDelete.AsEnumerable());
        }
    }
}