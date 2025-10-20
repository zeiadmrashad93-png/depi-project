using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using befit.core.Contracts;
using befit.dataAccess.Repositories;

namespace befit.dataAccess.Data
{
    internal class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _db;
        private Lazy<MenuItemRepository> _menuItemRepository;

        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            _menuItemRepository = new Lazy<MenuItemRepository>(new MenuItemRepository(db));
        }
        public IMenuItemRepository MenuItemRepository => _menuItemRepository.Value;

        public async Task SaveChanges()
        {
            await _db.SaveChangesAsync();
        }
    }
}
