using System.Security.Claims;
using befit.application.Contracts;
using befit.application.DTOs.MenuItems;
using befit.application.Enums;
using befit.application.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace befit.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemsController : ControllerBase
    {
        private IMenuItemsService _menuItemsService;
        public MenuItemsController(IMenuItemsService menuItemsService)
        {
            _menuItemsService = menuItemsService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            Claim? roleClaim = User.FindFirst(ClaimTypes.Role);
            Roles? role = roleClaim is null? null:Enum.Parse<Roles>(roleClaim.Value);
            object? result = await _menuItemsService.GetMenuItemById(id, role);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] MenuItemsUserOptions options)
        {
            IEnumerable<MenuItemListUserDTO> result = await _menuItemsService.GetMenuItemsListForUser(options);

            if (result.Count() == 0)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [Route("admin")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Get([FromQuery] MenuItemsAdminOptions options)
        {
            IEnumerable<MenuItemListAdminDto> result =await _menuItemsService.GetMenuItemsListForAdmin(options);
            
            if (result.Count() == 0)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [Route("chef")]
        [Authorize(Roles = "CHEF")]
        public async Task<IActionResult> Get([FromQuery] MenuItemsChefOptions options)
        {
            IEnumerable<MenuItemListChefDto> result = await _menuItemsService.GetMenuItemsListForChef(options);

            if (result.Count() == 0)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Post([FromBody] MenuItemInsertRequestDto menuItem)
        {
            return Ok(await _menuItemsService.CreateNewMenuItem(menuItem));
        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Put([FromBody] MenuItemUpdateDto menuItem)
        {
            MenuItemUpdateDto? updatedMenuItem = await _menuItemsService.UpdateMenuItem(menuItem);

            if (updatedMenuItem == null)
                return NotFound();

            return Ok(updatedMenuItem);
        }

        [HttpDelete]
        [Authorize(Roles = "ADMIN")]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            MenuItemDeleteDto? deletedMenuItem = await _menuItemsService.DeleteMenuItem(id);

            if (deletedMenuItem == null)
                return NotFound();

            return Ok(deletedMenuItem);
        }
    }
}
