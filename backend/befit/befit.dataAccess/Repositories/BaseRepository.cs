using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using befit.core.Contracts;
using befit.core.Entities;
using befit.dataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Linq.Dynamic.Core;

namespace befit.dataAccess.Repositories
{
    internal class BaseRepository<TEntity, TID> : IBaseRepository<TEntity, TID> where TEntity : Entity<TID>
    {
        protected AppDbContext _db;
        protected DbSet<TEntity> dbSet;

        public BaseRepository(AppDbContext db)
        {
            _db = db;
            dbSet = db.Set<TEntity>();
        }
        public async Task Create(TEntity entity)
        {
           await dbSet.AddAsync(entity);
        }

        public async Task<TEntity?> Delete(int id)
        {
            TEntity? entityToDelete = await dbSet.FindAsync(id);

            if (entityToDelete != null)
                dbSet.Remove(entityToDelete);

            return entityToDelete;
        }

        public async Task<IEnumerable<TResult>> GetAll<TResult>(IBaseSpecification<TEntity, TID, TResult> specification)
        {
            IQueryable<TEntity> data = dbSet;

            return await GetQuery(data, specification)
                .Select(specification.Selector)
                .ToListAsync(); ;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<TEntity?> GetById(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<TResult?> GetById<TResult>(TID id, IProjectionSpecification<TEntity, TID, TResult> specification)
        {
            return await dbSet
                    .Where(d => d.Id.Equals(id))
                    .Select(specification.Selector)
                    .SingleOrDefaultAsync();
        }

        public void Update(TEntity entity)
        {
            dbSet.Update(entity);
        }

        private IQueryable<TEntity> GetQuery<TResult>(IQueryable<TEntity> data, IBaseSpecification<TEntity, TID, TResult> specification)
        {
            if (specification.Criteria != null)
                data = data.Where(specification.Criteria);

            foreach (var include in specification.Includes)
                data = data.Include(include);

            if (specification.OrderBy != null)
            {

                if (specification.IsAscending is null)
                    specification.IsAscending = true;

                data = ((bool)specification.IsAscending)? data.OrderBy(specification.OrderBy) : data.OrderBy(specification.OrderBy + " descending");
            }

            if (specification.IsPaginationEnabled)
                data = data.Skip((specification.PageNo - 1) * specification.PageSize)
                    .Take(specification.PageSize);

            return data;
        }
    }
}
