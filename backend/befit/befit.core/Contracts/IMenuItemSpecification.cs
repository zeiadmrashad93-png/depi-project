using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using befit.core.Entities;

namespace befit.core.Contracts
{
    public interface IMenuItemSpecification<TResult>:IBaseSpecification<MenuItem, int, TResult>
    {
    }
}
