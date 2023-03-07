using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PresentationLayer.Models;
using System.Reflection.Metadata;
using System.Security.Claims;

namespace PresentationLayer.Controllers
{
	public class WriterController : Controller
	{
        UserManager um = new UserManager(new EfUserRepository());
        private readonly UserManager<AppUser> _userManager;
		public WriterController(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}

		[HttpGet]
		public IActionResult Index()
		{
            GetListCities();
            var user = um.GetByUserName(User.Identity.Name).FirstOrDefault();

            UserUpdateViewModel model = new UserUpdateViewModel();
            model.namesurname = user.NameSurname;
            model.phonenumber = user.PhoneNumber;
            model.mail = user.Email;
            model.image = user.ImageUrl;
            return View(model);
		}
        [HttpPost]

        public async Task<IActionResult> Index(UserUpdateViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            user.PhoneNumber = model.phonenumber;
            user.Email = model.mail;
            user.NameSurname = model.namesurname;
            if (model.password !=null)
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user,model.password);
            }
            if (model.image != null)
            {
                user.ImageUrl = model.image;
            }

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                 return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }
        }

		public PartialViewResult WriterNavPartial()
		{
            return PartialView();
		}
        public PartialViewResult WriterFooterPartial()
        {
            return PartialView();
        }
        public void GetListCities()
        {
            City c = new City();
            List<SelectListItem> deger1 = (from x in c.RegisterCities()
                                           select new SelectListItem
                                           {
                                               Text = x.CityName,   //optionun metni
                                               Value = x.CityName //optionun valuesi
                                           }).ToList();
            ViewBag.dpr = deger1;
        }
    }
}
