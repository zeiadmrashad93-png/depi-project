using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using befit.domain.Entities;

namespace befit.domain.Contracts
{
    public interface ICategoryRepository:IBaseRepository<Category, int>
    {
    }
}
