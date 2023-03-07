using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [AllowAnonymous]
    public class AboutController : Controller
    {
        AboutManager am = new AboutManager(new EfAboutRepository());
        public IActionResult AboutUs()
        {
            var about = am.GetListActive();
            return View(about);
        }
        public PartialViewResult PartialSocialMedia()
        {
            return PartialView();
        }
    }
}
