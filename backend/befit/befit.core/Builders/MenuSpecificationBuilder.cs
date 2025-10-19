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
    public class MenuSpecificationBuilder<TResult> : SpecificationBuilder<IMenuItemSpecification<TResult>, MenuItem, int, TResult>,
        IMenuSpecificationBuilder<TResult>
    {
        private IMenuItemSpecification<TResult>? menuItemSpecification;

        public override ISpecificationBuilder<IMenuItemSpecification<TResult>, MenuItem, int, TResult> Reset()
        {
            menuItemSpecification = new MenuItemSpecification<TResult>();
            return this;
        }

        public IMenuSpecificationBuilder<TResult> SetCategoryId(int categoryId)
        {
            AddCriteria(m => m.CategoryId == categoryId);
            return this;
        }
    }
}
