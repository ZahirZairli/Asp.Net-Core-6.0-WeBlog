using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [AllowAnonymous]
    public class ContactController : Controller
	{
		ContactManager cm = new ContactManager(new EfContactRepository());

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}
        [HttpPost]
        public IActionResult Index(Contact c)
        {
			cm.TAdd(c);
			TempData["contactmessage"] = "Sent";
            return RedirectToAction("Index", "Contact");
        }
    }
}
