using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using befit.core.Entities;

namespace befit.core.Contracts
{
    public interface IBaseSpecification<TEntity,TId, TResult>:IProjectionSpecification<TEntity, TId, TResult>
        where TEntity : Entity<TId>
    {
        Expression<Func<TEntity, bool>>? Criteria { get; set; }
        IEnumerable<Expression<Func<TEntity, object>>> Includes { get; set; }
        string? OrderBy { get; set; }
        bool? IsAscending { get; set; }
        int PageNo { get; set; }
        int PageSize { get; set; }
        bool IsPaginationEnabled { get; set; }
    }
}
