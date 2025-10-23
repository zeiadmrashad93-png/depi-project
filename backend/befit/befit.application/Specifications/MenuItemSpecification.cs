using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using befit.domain.Contracts;
using befit.domain.Entities;


namespace befit.application.Specifications
{
    public class MenuItemSpecification<TResult>:BaseSpecification<MenuItem, int, TResult>, IMenuItemSpecification<TResult>
    {
        public MenuItemSpecification()
        {
            
        }
        
    }
}
