using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace PresentationLayer.Controllers
{
    public class DashboardController : Controller
    {
        BlogManager bm = new BlogManager(new EfBlogRepository());
        CategoryManager cm = new CategoryManager(new EfCategoryRepository());
        UserManager um = new UserManager(new EfUserRepository());

		public IActionResult Index()
        {
            var writer = um.GetByUserName(User.Identity.Name).FirstOrDefault();
            var userid = writer.Id;
            ViewBag.blogscount = bm.GetListActive().Count();   
            ViewBag.yoursblogscount = bm.GetListWithWriter(userid).Count();
            ViewBag.categorycount= cm.GetListActive().Count();
            ViewBag.userId = userid;
            return View();
        }
    }
}
