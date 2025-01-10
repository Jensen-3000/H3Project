using H3Project.Data.DTOs.UserRoles;
using H3Project.Data.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace H3Project.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserRolesController : ControllerBase
    {
        private readonly IUserRoleService _userRoleService;

        public UserRolesController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserRoleSimpleDto>>> GetAll()
            => Ok(await _userRoleService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<UserRoleWithUsersDto>> GetById(int id)
            => Ok(await _userRoleService.GetByIdAsync(id));

        [HttpPost]
        public async Task<ActionResult<UserRoleSimpleDto>> Create(UserRoleCreateDto createDto)
        {
            var role = await _userRoleService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = role.Id }, role);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UserRoleUpdateDto updateDto)
        {
            await _userRoleService.UpdateAsync(id, updateDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userRoleService.DeleteAsync(id);
            return NoContent();
        }
    }
}
