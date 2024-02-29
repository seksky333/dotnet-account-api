using AccountAPI.Network.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AccountAPI.Controllers;

[ApiController]
[Route("/")]
public class UserManagerController(UserManager<IdentityUser> userManager) : ControllerBase
{
    private const string EndpointName = "user-role-management";

    [HttpPost(EndpointName)]
    public async Task<IActionResult> AddRoleToUser(
        [FromBody] UserRoleRequest request)
    {
        var user = await userManager.FindByIdAsync(request.UserId);
        if (user != null)
        {
            try
            {
                var result = await userManager.AddToRoleAsync(user, request.RoleName);
                if (result.Succeeded)
                {
                    return NoContent();
                }

                return Problem();
            }
            catch (Exception e)
            {
                return Problem(detail: e.Message);
            }
        }

        return NotFound("User cannot be found!");
    }

    [HttpDelete(EndpointName)]
    public async Task<IActionResult> RemoveUserFromRole(
        [FromBody] UserRoleRequest request)
    {
        var user = await userManager.FindByIdAsync(request.UserId);
        if (user != null)
        {
            try
            {
                var result = await userManager.RemoveFromRoleAsync(user, request.RoleName);
                if (result.Succeeded)
                {
                    return NoContent();
                }

                return Problem();
            }
            catch (Exception e)
            {
                return Problem(detail: e.Message);
            }
        }

        return NotFound("User cannot be found!");
    }
}