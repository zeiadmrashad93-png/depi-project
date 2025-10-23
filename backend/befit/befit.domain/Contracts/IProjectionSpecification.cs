using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using befit.domain.Entities;

namespace befit.domain.Contracts
{
    public interface IProjectionSpecification<TEntity,TId, TResult> where TEntity : Entity<TId>
    {
        Expression<Func<TEntity, TResult>>? Selector { get; set; }
    }
}
