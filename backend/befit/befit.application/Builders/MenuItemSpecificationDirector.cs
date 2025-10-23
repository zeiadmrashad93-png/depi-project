using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using befit.domain.Contracts;
using befit.domain.Entities;

namespace befit.application.Builders
{
    public class MenuItemSpecificationDirector<TResult>
    {
        IMenuSpecificationBuilder<TResult> _builder;
        public MenuItemSpecificationDirector(IMenuSpecificationBuilder<TResult> builder)
        {
            _builder = builder;
        }

        public IMenuItemSpecification<TResult> MakeProjectionOnlySpecification(Expression<Func<MenuItem, TResult>> selector)
        {
            return _builder.Reset()
                .SetSelector(selector)
                .GetSpecification();
        }

        public IMenuItemSpecification<TResult> MakePaginatedSpecificationWithFilters
            (int pageNo, int pageSize, int categoryId,
            IEnumerable<Expression<Func<MenuItem, bool>>> criteria,
            string? orderBy, bool? isAscending,
            Expression<Func<MenuItem, TResult>> selector)
        {
            var result = ((IMenuSpecificationBuilder<TResult>)_builder.Reset())
                .SetCategoryId(categoryId)
                .SetOrderBy(orderBy, isAscending)
                .SetPagination(pageNo, pageSize)
                .SetSelector(selector);

            foreach (var item in criteria)
                result.AddCriteria(item);

            return result.GetSpecification();
        }
    }
}
