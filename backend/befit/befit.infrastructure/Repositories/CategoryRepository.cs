using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using befit.domain.Contracts;
using befit.domain.Entities;
using befit.infrastructure.Data;

namespace befit.infrastructure.Repositories
{
    internal class CategoryRepository : BaseRepository<Category, int>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext db) : base(db)
        {
        }
    }
}
