using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace befit.dataAccess.Contracts
{
    public interface IBaseRepository<TEntity, TID> where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        TEntity? GetById(int id);
        void Create(TEntity entity);
        void Update(TEntity entity);
        TEntity? Delete(int id);
    }
}
