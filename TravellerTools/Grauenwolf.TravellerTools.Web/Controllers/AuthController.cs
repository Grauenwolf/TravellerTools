using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Grauenwolf.TravellerTools.Web.Controllers;

[Route("api/auth")]
[ApiController]
[AllowAnonymous]
public class AuthController : ControllerBase
{
	[HttpPost("login")]
	public async Task<IActionResult> Login([FromForm] string password, [FromForm] string? returnUrl)
	{
		if (password != "Traveller")
		{
			return Redirect($"/login?error=true&returnUrl={Uri.EscapeDataString(returnUrl ?? "/")}");
		}

		var claims = new List<Claim>
		{
			new(ClaimTypes.Name, "User"),
		};

		var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
		var principal = new ClaimsPrincipal(identity);

#if DEBUG
		await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
			new AuthenticationProperties { IsPersistent = false });
#else
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
            new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTime.Now.AddDays(300) });
#endif
		return Redirect(returnUrl ?? "/");
	}

	[HttpGet("logout")]
	public async Task<IActionResult> Logout()
	{
		await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
		return Redirect("/login");
	}
}
