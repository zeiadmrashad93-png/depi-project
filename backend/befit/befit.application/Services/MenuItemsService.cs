using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using befit.application.Contracts;
using befit.application.DTOs.MenuItems;
using befit.application.Enums;
using befit.application.Options;
using befit.core.Builders;
using befit.core.Contracts;
using befit.core.Entities;
using befit.core.Specifications;
using CloudinaryDotNet.Actions;

namespace befit.application.Services
{
    internal class MenuItemsService : IMenuItemsService
    {
        private IUnitOfWork _unitOfWork;
        private IMenuItemRepository repository;

        public MenuItemsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            repository = _unitOfWork.MenuItemRepository;
        }

        public async Task<object?> GetMenuItemById(int id, Roles? role)
        {
            object? dto=null;
            MenuItem? menuItemEntity = await repository.GetById(id);

            if (menuItemEntity is null)
                return null;

            switch(role)
            {
                case null:
                case Roles.USER:
                    dto = GetMenuItemDetailForUser(id);
                    break;
                case Roles.ADMIN:
                    break;
                case Roles.CHEF:
                    break;
                case Roles.DELIVERY_GUY:
                    break;
            }

            return dto;
        }

        public async Task<IEnumerable<MenuItemListUserDTO>> GetMenuItemsListForUser(MenuItemsUserOptions options)
        {
            MenuSpecificationBuilder<MenuItemListUserDTO> builder = new MenuSpecificationBuilder<MenuItemListUserDTO>();
            MenuItemSpecificationDirector<MenuItemListUserDTO> director
                = new MenuItemSpecificationDirector<MenuItemListUserDTO>(builder);

            var criteria = new List<Expression<Func<MenuItem, bool>>>();

            if (options.MaxProtein != null)
                criteria.Add(m => m.Protein < options.MaxProtein);
            
            if (options.MinProtein != null)
                criteria.Add(m => m.Protein > options.MinProtein);

            if (options.MaxCarbohydrates != null)
                criteria.Add(m => m.Carbohydrates < options.MaxCarbohydrates);
            
            if (options.MinCarbohydrates != null)
                criteria.Add(m => m.Carbohydrates > options.MinCarbohydrates);

            if (options.MaxFat != null)
                criteria.Add(m => m.Fats < options.MaxFat);
            
            if (options.MinFat != null)
                criteria.Add(m => m.Fats > options.MinFat);



            IMenuItemSpecification<MenuItemListUserDTO> specification = director.MakePaginatedSpecificationWithFilters(options.PageNo, options.ItemsPerPage, options.CategoryId,
                criteria, options.SortBy, options.IsAscending, m => new MenuItemListUserDTO
                {
                    Id = m.Id,
                    Name = m.Name,
                    Picture = m.Picture,
                    Calories = m.Calories,
                    Protein = m.Protein,
                    Fats = m.Fats,
                    Carbohydrates = m.Carbohydrates
                });

            return await repository.GetAll(specification);
        }

        public Task<IEnumerable<MenuItemListAdminDto>> GetMenuItemsListForAdmin(MenuItemsAdminOptions options)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MenuItemListChefDto>> GetMenuItemsListForChef(MenuItemsChefOptions options)
        {
            throw new NotImplementedException();
        }

        public Task<MenuItemInsertResponseDto> CreateNewMenuItem(MenuItemInsertRequestDto menuItemInsertRequestDto)
        {
            throw new NotImplementedException();
        }

        public Task<MenuItemUpdateDto?> UpdateMenuItem(MenuItemUpdateDto menuItemUpdateDto)
        {
            throw new NotImplementedException();
        }

        public Task<MenuItemDeleteDto?> DeleteMenuItem(MenuItemDeleteDto menuItemDeleteDto)
        {
            throw new NotImplementedException();
        }

        private async Task<MenuItemDetailUserDTO?> GetMenuItemDetailForUser(int id)
        {
            MenuSpecificationBuilder<MenuItemDetailUserDTO> builder = new MenuSpecificationBuilder<MenuItemDetailUserDTO>();
            MenuItemSpecificationDirector<MenuItemDetailUserDTO> director
                = new MenuItemSpecificationDirector<MenuItemDetailUserDTO>(builder);

            return await repository.GetById(id, director.MakeProjectionOnlySpecification(m => new MenuItemDetailUserDTO
            {
                Id = id,
                Name = m.Name,
                Description = m.Description,
                Picture = m.Picture,
                Calories = m.Calories,
                Fats = m.Fats,
                Protein = m.Protein,
                Carbohydrates = m.Carbohydrates
            }));
        }
    }
}
