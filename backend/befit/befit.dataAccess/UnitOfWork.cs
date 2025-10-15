using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using befit.dataAccess.Contracts;
using befit.dataAccess.Data;

namespace befit.dataAccess
{
    internal class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _db;
        public UnitOfWork(AppDbContext db)
        {
            _db = db;
        }
        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
