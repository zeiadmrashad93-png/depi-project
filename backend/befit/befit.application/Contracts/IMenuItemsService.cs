using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using befit.application.DTOs.MenuItems;
using befit.application.Enums;
using befit.application.Options;
using befit.domain.Entities;

namespace befit.application.Contracts
{
    public interface IMenuItemsService
    {
        Task<IEnumerable<MenuItemListUserDTO>> GetMenuItemsListForUser(MenuItemsUserOptions options);
        Task<IEnumerable<MenuItemListAdminDto>> GetMenuItemsListForAdmin(MenuItemsAdminOptions options);
        Task<IEnumerable<MenuItemListChefDto>> GetMenuItemsListForChef(MenuItemsChefOptions options);
        Task<MenuItemInsertResponseDto> CreateNewMenuItem(MenuItemInsertRequestDto menuItemInsertRequestDto);
        Task<MenuItemUpdateResponseDto?> UpdateMenuItem(MenuItemUpdateRequestDto menuItemUpdateDto);
        Task DeleteMenuItem(int id);
        Task<MenuItemDetailChefDTO?> GetMenuItemDetailForUser(int id);
        Task<MenuItemDetailChefDto?> GetMenuItemDetailForChef(int id);
        Task<MenuItemDetailAdminDto?> GetMenuItemDetailForAdmin(int id);

    }
}
