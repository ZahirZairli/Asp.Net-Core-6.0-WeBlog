using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Areas.Admin.ViewComponents.Statistic
{
    public class Statistic2 : ViewComponent
    {
        BlogManager bm = new BlogManager(new EfBlogRepository());
        public IViewComponentResult Invoke()
        {
            var blog = bm.GetListLast().FirstOrDefault();
            ViewBag.blogtitle = blog.BlogTitle;
            ViewBag.blogid= blog.BlogId;
            ViewBag.blogview= blog.BlogViewingCount;
            return View();
        }
    }
}
