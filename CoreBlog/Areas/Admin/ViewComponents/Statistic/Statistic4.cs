using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Areas.Admin.ViewComponents.Statistic
{
    public class Statistic4 : ViewComponent
    {
        BlogManager bm = new BlogManager(new EfBlogRepository());
        UserManager um = new UserManager(new EfUserRepository());
        public IViewComponentResult Invoke()
        {
            var user = um.GetByUserName(User.Identity.Name).FirstOrDefault();
            return View(user);
        }
    }
}
