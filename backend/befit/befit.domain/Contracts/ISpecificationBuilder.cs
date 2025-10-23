using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using befit.domain.Entities;

namespace befit.domain.Contracts
{
    public interface ISpecificationBuilder<TSpecification, TEntity, TId, TResult> 
        where TSpecification : IBaseSpecification<TEntity, TId, TResult> where TEntity : Entity<TId>
    {
        public ISpecificationBuilder<TSpecification, TEntity, TId, TResult> Reset();


        public ISpecificationBuilder<TSpecification, TEntity, TId, TResult> 
            AddCriteria(Expression<Func<TEntity, bool>> criteria);


        public ISpecificationBuilder<TSpecification, TEntity, TId, TResult> 
            SetSelector(Expression<Func<TEntity, TResult>> selector);


        public ISpecificationBuilder<TSpecification, TEntity, TId, TResult> 
            SetOrderBy(string? key, bool? isAscending);


        public ISpecificationBuilder<TSpecification, TEntity, TId, TResult> 
            SetPagination(int pageNo, int pageSize);


        public ISpecificationBuilder<TSpecification, TEntity, TId, TResult>
            SetIncludes(IEnumerable<Expression<Func<TEntity, object>>> includes);

        public TSpecification GetSpecification();
    }
}
