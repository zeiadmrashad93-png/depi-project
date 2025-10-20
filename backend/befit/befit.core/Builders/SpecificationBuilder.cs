using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using befit.core.Contracts;
using befit.core.Entities;
using befit.core.ExpressionHelpers;
using befit.core.Specifications;

namespace befit.core.Builders
{
    public abstract class SpecificationBuilder<TSpecification, TEntity, TId, TResult>
        : ISpecificationBuilder<TSpecification, TEntity, TId, TResult> 
        where TEntity: Entity<TId>
        where TSpecification:IBaseSpecification<TEntity, TId, TResult>
    {
        private TSpecification? specification;
        public TSpecification GetSpecification()
        {
            return specification;
        }

        public abstract ISpecificationBuilder<TSpecification, TEntity, TId, TResult> Reset();

        public ISpecificationBuilder<TSpecification, TEntity, TId, TResult> AddCriteria(Expression<Func<TEntity, bool>> criteria)
        {
            if (criteria == null)
                return this;

            if (specification.Criteria == null)
                specification.Criteria = criteria;
            else
                specification.Criteria = specification?.Criteria.And(criteria);
            return this;
        }

        public ISpecificationBuilder<TSpecification, TEntity, TId, TResult> SetIncludes(IEnumerable<Expression<Func<TEntity, object>>> includes)
        {
            specification.Includes = includes;
            return this;
        }

        public ISpecificationBuilder<TSpecification, TEntity, TId, TResult> SetOrderBy(string? key, bool? isAscending)
        {
            specification.OrderBy = key;
            specification.IsAscending = isAscending;
            return this;
        }

        public ISpecificationBuilder<TSpecification, TEntity, TId, TResult> SetPagination(int pageNo, int pageSize)
        {
            specification.IsPaginationEnabled = true;
            specification.PageNo = pageNo;
            specification.PageSize = pageSize;
            return this;
        }

        public ISpecificationBuilder<TSpecification, TEntity, TId, TResult> SetSelector(Expression<Func<TEntity, TResult>> selector)
        {
            specification.Selector = selector;
            return this;
        }
    }
}
