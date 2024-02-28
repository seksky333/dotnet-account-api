using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AccountAPI.Controllers;

[ApiController]
[Route("/")]
public class UserController : ControllerBase
{
    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout(
        SignInManager<IdentityUser> signInManager,
        [FromBody] object? empty)
    {
        if (empty != null)
        {
            await signInManager.SignOutAsync();
            return Ok();
        }
        return NoContent();
    }
}