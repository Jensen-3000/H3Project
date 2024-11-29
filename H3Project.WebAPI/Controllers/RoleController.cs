using H3Project.Data.Models.Domain;
using H3Project.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace H3Project.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly RoleRepository _roleRepository;

    public RoleController(RoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetRoles()
    {
        var roles = await _roleRepository.GetAllRolesAsync();
        return Ok(roles);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRole(int id)
    {
        var role = await _roleRepository.GetRoleByIdAsync(id);

        if (role == null)
        {
            return NotFound();
        }

        return Ok(role);
    }

    [HttpPost]
    public async Task<IActionResult> PostRole(Role role)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _roleRepository.AddRoleAsync(role);

        return CreatedAtAction(nameof(GetRole), new { id = role.RoleId }, role);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutRole(int id, Role role)
    {
        if (id != role.RoleId)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _roleRepository.UpdateRoleAsync(role);
        }
        catch (Exception)
        {
            var exists = await _roleRepository.GetRoleByIdAsync(id);
            if (exists == null)
            {
                return NotFound();
            }

            return BadRequest();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRole(int id)
    {
        await _roleRepository.DeleteRoleAsync(id);
        return NoContent();
    }
}
