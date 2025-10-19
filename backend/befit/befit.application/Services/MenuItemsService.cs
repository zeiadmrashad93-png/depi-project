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
        private IFileService _fileService;

        public MenuItemsService(IUnitOfWork unitOfWork, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            repository = _unitOfWork.MenuItemRepository;
            _fileService = fileService;
        }

        public async Task<object?> GetMenuItemById(int id, Roles? role)
        {
            object? dto = null;
            MenuItem? menuItemEntity = await repository.GetById(id);

            if (menuItemEntity is null)
                return null;

            switch(role)
            {
                case null:
                case Roles.USER:
                    dto = await GetMenuItemDetailForUser(id);
                    break;
                case Roles.ADMIN:
                    dto = await GetMenuItemDetailForAdmin(id);
                    break;
                case Roles.CHEF:
                    dto = await GetMenuItemDetailForChef(id);
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

        public async Task<IEnumerable<MenuItemListAdminDto>> GetMenuItemsListForAdmin(MenuItemsAdminOptions options)
        {
            var builder = new MenuSpecificationBuilder<MenuItemListAdminDto>();
            var director = new MenuItemSpecificationDirector<MenuItemListAdminDto>(builder);

            var criteria = new List<Expression<Func<MenuItem, bool>>>();

            if (options.MaxPrice != null)
                criteria.Add(m => m.Price < options.MaxPrice);

            if (options.MinPrice != null)
                criteria.Add(m => m.Price > options.MinPrice);

            var specification = director.MakePaginatedSpecificationWithFilters(options.PageNo, options.ItemsPerPage, options.CategoryId,
               criteria, options.SortBy, options.IsAscending, m => new MenuItemListAdminDto
               {
                   Id = m.Id,
                   Name = m.Name,
                   CategoryId = m.CategoryId,
                   Price = m.Price,
                   Picture = m.Picture,
               });

            return await repository.GetAll(specification);
        }

        public async Task<IEnumerable<MenuItemListChefDto>> GetMenuItemsListForChef(MenuItemsChefOptions options)
        {
            var builder = new MenuSpecificationBuilder<MenuItemListChefDto>();
            var director = new MenuItemSpecificationDirector<MenuItemListChefDto>(builder);

            var specification = director.MakePaginatedSpecificationWithFilters(options.PageNo, options.ItemsPerPage, options.CategoryId,
                Enumerable.Empty<Expression<Func<MenuItem, bool>>>(), options.SortBy, options.IsAscending, m => new MenuItemListChefDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    PreparationTime = m.PreparationTime,
                    Picture = m.Picture
                });

            return await repository.GetAll(specification);
        }

        public Task<MenuItemInsertResponseDto> CreateNewMenuItem(MenuItemInsertRequestDto menuItemInsertRequestDto)
        {
            throw new NotImplementedException();
        }

        public Task<MenuItemUpdateDto?> UpdateMenuItem(MenuItemUpdateDto menuItemUpdateDto)
        {
            throw new NotImplementedException();
        }

        public Task<MenuItemDeleteDto?> DeleteMenuItem(int id)
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

        private async Task<MenuItemDetailChefDto?> GetMenuItemDetailForChef(int id)
        {
            var builder = new MenuSpecificationBuilder<MenuItemDetailChefDto>();
            var director = new MenuItemSpecificationDirector<MenuItemDetailChefDto>(builder);

            var specification = director.MakeProjectionOnlySpecification(m => new MenuItemDetailChefDto
            {
                Id = m.Id,
                Name = m.Name,
                Recipe = m.Recipe,
                Picture = m.Picture,
                PreparationTime = m.PreparationTime,
                Video = m.Video
            });

            return await repository.GetById(id, specification);
        }

        private async Task<MenuItemDetailAdminDto?> GetMenuItemDetailForAdmin(int id)
        {
            var builder = new MenuSpecificationBuilder<MenuItemDetailAdminDto>();
            var director = new MenuItemSpecificationDirector<MenuItemDetailAdminDto>(builder);

            var specification = director.MakeProjectionOnlySpecification( m => new MenuItemDetailAdminDto
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                Calories = m.Calories,
                Carbohydrates = m.Carbohydrates,
                CategoryId = m.CategoryId,
                Fats = m.Fats,
                Picture = m.Picture,
                PreparationTime = m.PreparationTime,
                Price = m.Price,
                Video = m.Video,
                Recipe = m.Recipe,
                Protein = m.Protein
            });

            return await repository.GetById(id, specification);
        }
    }
}
