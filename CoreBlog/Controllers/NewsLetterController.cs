using Azure.Core;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [AllowAnonymous]
    public class NewsLetterController : Controller
	{
		NewsLetterManager nm = new NewsLetterManager(new EfNewsLetterRepository());
        [HttpGet]
        public PartialViewResult PartialSubscribeMail()
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult PartialSubscribeMail([FromForm] NewsLetter p)
        {
            nm.TAdd(p);
            return PartialView();
        }

    }
}
