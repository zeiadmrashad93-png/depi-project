using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using befit.core.Contracts;
using befit.core.Entities;
using befit.core.ExpressionHelpers;


namespace befit.core.Specifications
{
    public class MenuItemSpecification<TResult>:BaseSpecification<MenuItem, int, TResult>, IMenuItemSpecification<TResult>
    {
        public MenuItemSpecification()
        {
            
        }
        
    }
}
