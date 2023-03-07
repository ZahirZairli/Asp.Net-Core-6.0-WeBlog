using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using System.Security.Claims;

namespace PresentationLayer.Controllers
{
	//User.FindFirstValue(ClaimTypes.NameIdentifier);
	[AllowAnonymous]
	public class LoginController : Controller
	{
		private readonly SignInManager<AppUser> _signInManager;
        public LoginController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpGet]
		public IActionResult SignIn()
		{
			return View();
		}

		[HttpPost]	
		public async Task<IActionResult> SignIn(UserSignInViewModel p)
		{
			if (ModelState.IsValid)
			{
				var result = await _signInManager.PasswordSignInAsync(p.username, p.password,false,true);
				if (result.Succeeded)
				{
                    return RedirectToAction("Index", "Dashboard");
                }
				if (result.IsLockedOut)
				{
					TempData["message"] = "false";
				}
				else
				{
					TempData["loginmessage"] = "false";
					return RedirectToAction("SignIn","Login");
				}
			}
			TempData["loginmessage"] = "false";
			return View(p);
		}

        public async Task<IActionResult> Logout()
        {
			await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Blog");
        }
		public IActionResult AccessDenied()
		{
			return View();
		}
    }
}

