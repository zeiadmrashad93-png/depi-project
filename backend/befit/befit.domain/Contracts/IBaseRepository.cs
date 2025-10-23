using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using befit.domain.Entities;

namespace befit.domain.Contracts
{
    public interface IBaseRepository<TEntity, TID> where TEntity : Entity<TID>
    {
        Task<IEnumerable<TResult>> GetAll<TResult>(IBaseSpecification<TEntity, TID, TResult> specification);
        Task<IEnumerable<TEntity>> GetAll();
        Task<TResult?> GetById<TResult>(TID id, IProjectionSpecification<TEntity, TID, TResult> specification);
        Task<TEntity?> GetById(int id);
        Task Create(TEntity entity);
        void Update(TEntity entity);
        Task<TEntity?> Delete(int id);
    }
}
