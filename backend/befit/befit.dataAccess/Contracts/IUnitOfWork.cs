using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace befit.dataAccess.Contracts
{
    public interface IUnitOfWork
    {

        void SaveChanges();
    }
}
