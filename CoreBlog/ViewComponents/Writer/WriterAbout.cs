using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PresentationLayer.ViewComponents.Writer
{
    public class WriterAbout : ViewComponent
    {
        UserManager um = new UserManager(new EfUserRepository());
        private readonly UserManager<AppUser> _userManager;

		public WriterAbout(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}

		public IViewComponentResult Invoke()
        {
            var user = um.GetByUserName(User.Identity.Name).FirstOrDefault();
            return View(user);
        }
    }
}
