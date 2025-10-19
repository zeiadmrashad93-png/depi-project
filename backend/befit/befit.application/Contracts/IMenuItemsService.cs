using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using befit.application.DTOs.MenuItems;
using befit.application.Enums;
using befit.application.Options;
using befit.core.Entities;

namespace befit.application.Contracts
{
    public interface IMenuItemsService
    {
        Task<object?> GetMenuItemById(int id, Roles? role);
        Task<IEnumerable<MenuItemListUserDTO>> GetMenuItemsListForUser(MenuItemsUserOptions options);
        Task<IEnumerable<MenuItemListAdminDto>> GetMenuItemsListForAdmin(MenuItemsAdminOptions options);
        Task<IEnumerable<MenuItemListChefDto>> GetMenuItemsListForChef(MenuItemsChefOptions options);
        Task<MenuItemInsertResponseDto> CreateNewMenuItem(MenuItemInsertRequestDto menuItemInsertRequestDto);
        Task<MenuItemUpdateDto?> UpdateMenuItem(MenuItemUpdateDto menuItemUpdateDto);
        Task<MenuItemDeleteDto?> DeleteMenuItem(int id);
    }
}
