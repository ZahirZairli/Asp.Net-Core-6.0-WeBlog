using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.ViewComponents.Blog
{
    public class LastSixBlogs : ViewComponent
    {
        BlogManager bm = new BlogManager(new EfBlogRepository());
        public IViewComponentResult Invoke()
        {
            var lasttwoblog = bm.GetListWithCategory().OrderBy(x=>x.BlogDate).Take(6).ToList();
            return View(lasttwoblog);
        }
    }
}
