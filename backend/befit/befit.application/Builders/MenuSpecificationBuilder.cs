using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using befit.domain.Contracts;
using befit.domain.Entities;
using befit.application.Specifications;

namespace befit.application.Builders
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
