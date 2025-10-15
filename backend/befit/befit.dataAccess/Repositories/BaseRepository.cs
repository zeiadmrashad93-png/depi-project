using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using befit.dataAccess.Contracts;
using befit.dataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace befit.dataAccess.Repositories
{
    internal class BaseRepository<TEntity, TID> : IBaseRepository<TEntity, TID> where TEntity : class
    {
        protected AppDbContext _db;
        protected DbSet<TEntity> dbSet;

        public BaseRepository(AppDbContext db)
        {
            _db = db;
            dbSet = db.Set<TEntity>();
        }
        public void Create(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public TEntity? Delete(int id)
        {
            TEntity? entityToDelete = dbSet.Find(id);

            if (entityToDelete != null)
                dbSet.Remove(entityToDelete);

            return entityToDelete;
        }

        public IQueryable<TEntity> GetAll()
        {
            return dbSet;
        }

        public TEntity? GetById(int id)
        {
            return dbSet.Find(id);
        }

        public void Update(TEntity entity)
        {
            dbSet.Update(entity);
        }
    }
}
