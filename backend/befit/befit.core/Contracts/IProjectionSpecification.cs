using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using befit.core.Entities;

namespace befit.core.Contracts
{
    public interface IProjectionSpecification<TEntity,TId, TResult> where TEntity : Entity<TId>
    {
        Expression<Func<TEntity, TResult>>? Selector { get; set; }
    }
}
