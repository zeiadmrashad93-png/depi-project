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

            switch(role)
            {
                case null:
                case Roles.USER:
                    dto = await getMenuItemDetailForUser(id);
                    break;
                case Roles.ADMIN:
                    dto = await getMenuItemDetailForAdmin(id);
                    break;
                case Roles.CHEF:
                    dto = await getMenuItemDetailForChef(id);
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

        public async Task<MenuItemInsertResponseDto> CreateNewMenuItem(MenuItemInsertRequestDto menuItemInsertRequestDto)
        {
            var menuItemEntity = new MenuItem
            {
                Name = menuItemInsertRequestDto.Name,
                Description = menuItemInsertRequestDto.Description,
                Recipe = menuItemInsertRequestDto.Recipe,
                Calories = menuItemInsertRequestDto.Calories,
                Carbohydrates = menuItemInsertRequestDto.Carbohydrates,
                CategoryId = menuItemInsertRequestDto.CategoryId,
                Fats = menuItemInsertRequestDto.Fats,
                Protein = menuItemInsertRequestDto.Protein,
                PreparationTime = menuItemInsertRequestDto.PreparationTime,
                Price = menuItemInsertRequestDto.Price,
                Picture = await _fileService.Upload(menuItemInsertRequestDto.Picture),
                Video = menuItemInsertRequestDto.Video is null ? null : await _fileService.Upload(menuItemInsertRequestDto.Video)
            };

            await repository.Create(menuItemEntity);

            await _unitOfWork.SaveChanges();

            return new MenuItemInsertResponseDto
            {
                Id = menuItemEntity.Id,
                Name = menuItemEntity.Name,
                Description = menuItemEntity.Description,
                Recipe = menuItemEntity.Recipe,
                Calories = menuItemEntity.Calories,
                Carbohydrates= menuItemEntity.Carbohydrates,
                CategoryId = menuItemEntity.CategoryId,
                Fats = menuItemEntity.Fats,
                Protein = menuItemEntity.Protein,
                PreparationTime = menuItemEntity.PreparationTime,
                Price = menuItemEntity.Price,
                Picture = menuItemEntity.Picture,
                Video = menuItemEntity.Video
            };
        }

        public async Task<MenuItemUpdateResponseDto?> UpdateMenuItem(MenuItemUpdateRequestDto menuItemUpdateDto)
        {
            var menuItemEntity = new MenuItem()
            {
                Id = menuItemUpdateDto.Id,
                Name = menuItemUpdateDto.Name,
                Description = menuItemUpdateDto.Description,
                Recipe = menuItemUpdateDto.Recipe,
                Calories = menuItemUpdateDto.Calories,
                Carbohydrates = menuItemUpdateDto.Carbohydrates,
                CategoryId = menuItemUpdateDto.CategoryId,
                Fats = menuItemUpdateDto.Fats,
                Protein = menuItemUpdateDto.Protein,
                Picture = await _fileService.Upload(menuItemUpdateDto.Picture),
                Video = menuItemUpdateDto.Video == null? null : await _fileService.Upload(menuItemUpdateDto.Video),
                PreparationTime = menuItemUpdateDto.PreparationTime,
                Price = menuItemUpdateDto.Price
            };

            await _fileService.Delete(await repository.GetById(menuItemUpdateDto.Id, new MenuItemSpecification<string>
            {
                Selector = m => m.Picture
            }));

            repository.Update(menuItemEntity);

            await _unitOfWork.SaveChanges();

            return new MenuItemUpdateResponseDto
            {
                Id = menuItemEntity.Id,
                Name = menuItemEntity.Name,
                Description = menuItemEntity.Description,
                Recipe = menuItemEntity.Recipe,
                Calories = menuItemEntity.Calories,
                CategoryId = menuItemEntity.CategoryId,
                Fats = menuItemEntity.Fats,
                Protein = menuItemEntity.Protein,
                Carbohydrates = menuItemEntity.Carbohydrates,
                Picture = menuItemEntity.Picture,
                Video = menuItemEntity.Video,
                PreparationTime = menuItemEntity.PreparationTime,
                Price = menuItemEntity.Price
            };
        }

        public async Task DeleteMenuItem(int id)
        {
            var deletedEntity = await repository.Delete(id);

            await _unitOfWork.SaveChanges();

            if (deletedEntity != null)
            {
                await _fileService.Delete(deletedEntity.Picture);
                if (deletedEntity.Video != null)
                    await _fileService.Delete(deletedEntity.Video);
            }
        }

        private async Task<MenuItemDetailUserDTO?> getMenuItemDetailForUser(int id)
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

        private async Task<MenuItemDetailChefDto?> getMenuItemDetailForChef(int id)
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

        private async Task<MenuItemDetailAdminDto?> getMenuItemDetailForAdmin(int id)
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
