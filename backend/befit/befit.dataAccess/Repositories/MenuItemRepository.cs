using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using befit.core.Contracts;
using befit.dataAccess.Data;
using befit.core.Entities;
using Microsoft.EntityFrameworkCore;

namespace befit.dataAccess.Repositories
{
    internal class MenuItemRepository : BaseRepository<MenuItem, int>, IMenuItemRepository
    {
        public MenuItemRepository(AppDbContext db) : base(db)
        {
        }
    }
}
