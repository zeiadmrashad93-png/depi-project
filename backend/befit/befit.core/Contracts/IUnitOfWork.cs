using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace befit.core.Contracts
{
    public interface IUnitOfWork
    {
        IMenuItemRepository MenuItemRepository { get; }

        ICategoryRepository CategoryRepository { get; }

        Task SaveChanges();
    }
}
