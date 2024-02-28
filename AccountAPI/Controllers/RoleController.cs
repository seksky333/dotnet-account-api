using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AccountAPI.Controllers;

[ApiController]
[Route("/")]
public class RoleController(RoleManager<IdentityRole> roleManager) : ControllerBase
{
    [HttpPost("role")]
    public async Task<IActionResult> CreateRole(
        [FromBody] IdentityRole? newRole)
    {
        if (newRole == null) return NoContent();
        await roleManager.CreateAsync(newRole);
        return Ok();
    }

    [HttpPut("role")]
    public async Task<IActionResult> UpdateRole(
        [FromBody] IdentityRole? updatedRole)
    {
        if (updatedRole?.Id == null) return BadRequest("Role id should not be null!");
        var role = await roleManager.FindByIdAsync(updatedRole.Id);
        
        if (role == null) return NotFound();
        role.Name = updatedRole.Name;
        role.ConcurrencyStamp = updatedRole.ConcurrencyStamp;
        role.NormalizedName = updatedRole.NormalizedName;
        await roleManager.UpdateAsync(role);
        return NoContent();
    }

    [HttpGet("roleById")]
    public async Task<IActionResult> GetRoleById(String id)
    {
        var role = await roleManager.FindByIdAsync(id);
        if (role != null)
        {
            return Ok(role);
        }

        return NotFound();
    }
    
    [HttpGet("roleByName")]
    public async Task<IActionResult> GetRoleByName(String name)
    {
        var role = await roleManager.FindByNameAsync(name);
        if (role != null)
        {
            return Ok(role);
        }

        return NotFound();
    }

    [HttpDelete("role")]
    public async Task<IActionResult> DeleteRole(
        [FromBody] IdentityRole? newRole)
    {
        if (newRole == null) return NoContent();
        await roleManager.CreateAsync(newRole);
        return Ok();
    }
}