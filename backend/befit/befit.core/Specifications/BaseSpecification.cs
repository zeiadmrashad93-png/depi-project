using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using befit.core.Contracts;
using befit.core.Entities;

namespace befit.core.Specifications
{
    public abstract class BaseSpecification<TEntity, TId, TResult> : IBaseSpecification<TEntity, TId, TResult> where TEntity : Entity<TId>
    {
        public Expression<Func<TEntity, TResult>> Selector { get; set; }
        public Expression<Func<TEntity, bool>>? Criteria { get; set; }

        public IEnumerable<Expression<Func<TEntity, object>>> Includes { get; set; }

        public string? OrderBy { get; set; }

        public bool? IsAscending { get; set; } = true;

        public int PageNo { get; set; }

        public int PageSize { get;  set; }

        public bool IsPaginationEnabled { get; set; } = false;
    }
}
