using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using befit.domain.Contracts;
using befit.infrastructure.Repositories;

namespace befit.infrastructure.Data
{
    internal class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _db;
        private Lazy<MenuItemRepository> _menuItemRepository;
        private Lazy<CategoryRepository> _categoryRepository;

        public IMenuItemRepository MenuItemRepository => _menuItemRepository.Value;
        public ICategoryRepository CategoryRepository => _categoryRepository.Value;

        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            _menuItemRepository = new Lazy<MenuItemRepository>(new MenuItemRepository(db));
            _categoryRepository = new Lazy<CategoryRepository>(new CategoryRepository(db));
        }

        public async Task SaveChanges()
        {
            await _db.SaveChangesAsync();
        }
    }
}
