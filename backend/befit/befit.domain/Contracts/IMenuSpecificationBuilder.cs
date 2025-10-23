using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using befit.domain.Entities;

namespace befit.domain.Contracts
{
    public interface IMenuSpecificationBuilder<TResult>:
        ISpecificationBuilder<IMenuItemSpecification<TResult>, MenuItem, int, TResult>
    {
        IMenuSpecificationBuilder<TResult> SetCategoryId(int categoryId);
    }
}
