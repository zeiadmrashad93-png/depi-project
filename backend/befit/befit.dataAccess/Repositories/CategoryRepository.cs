using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using befit.core.Contracts;
using befit.core.Entities;
using befit.dataAccess.Data;

namespace befit.dataAccess.Repositories
{
    internal class CategoryRepository : BaseRepository<Category, int>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext db) : base(db)
        {
        }
    }
}
