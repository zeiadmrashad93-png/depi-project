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
        public async Task<IActionResult> GetForUser([FromRoute] int id)
        {
            MenuItemDetailChefDTO? result = await _menuItemsService.GetMenuItemDetailForUser(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [Route("admin/{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetForAdmin([FromRoute] int id)
        {
            MenuItemDetailAdminDto? result = await _menuItemsService.GetMenuItemDetailForAdmin(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [Route("chef/{id}")]
        [Authorize(Roles = "CHEF")]
        public async Task<IActionResult> GetForChef([FromRoute] int id)
        {
            MenuItemDetailChefDto? result = await _menuItemsService.GetMenuItemDetailForChef(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] MenuItemsUserOptions options)
        {
            IEnumerable<MenuItemListUserDTO> result = await _menuItemsService.GetMenuItemsListForUser(options);

            return Ok(result);
        }

        [HttpGet]
        [Route("admin")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Get([FromQuery] MenuItemsAdminOptions options)
        {
            IEnumerable<MenuItemListAdminDto> result =await _menuItemsService.GetMenuItemsListForAdmin(options);

            return Ok(result);
        }

        [HttpGet]
        [Route("chef")]
        [Authorize(Roles = "CHEF")]
        public async Task<IActionResult> Get([FromQuery] MenuItemsChefOptions options)
        {
            IEnumerable<MenuItemListChefDto> result = await _menuItemsService.GetMenuItemsListForChef(options);

            return Ok(result);
        }

        [HttpPost]
        [Route("admin")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Post([FromForm] MenuItemInsertRequestDto menuItem, IFormFile picture, IFormFile video)
        {
            menuItem.Picture = (FileStream)picture.OpenReadStream();
            menuItem.Video = (FileStream)video.OpenReadStream();

            return Ok(await _menuItemsService.CreateNewMenuItem(menuItem));
        }

        [HttpPut]
        [Route("admin")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Put([FromForm] MenuItemUpdateRequestDto menuItem, IFormFile picture, IFormFile video)
        {
            menuItem.Picture = (FileStream)picture.OpenReadStream();
            menuItem.Video = (FileStream)video.OpenReadStream();

            MenuItemUpdateResponseDto? updatedMenuItem = await _menuItemsService.UpdateMenuItem(menuItem);

            if (updatedMenuItem == null)
                return NotFound();

            return Ok(updatedMenuItem);
        }

        [HttpDelete]
        [Route("admin")]
        [Authorize(Roles = "ADMIN")]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _menuItemsService.DeleteMenuItem(id);

            return NoContent();
        }
    }
}
