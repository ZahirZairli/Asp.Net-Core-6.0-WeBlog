using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.ViewComponents.Blog
{
	public class BlogByWriter : ViewComponent
	{
		BlogManager bm = new BlogManager(new EfBlogRepository());
		UserManager um = new UserManager(new EfUserRepository());
        public IViewComponentResult Invoke(int id)
		{
			var writer = um.GetById(id);
			ViewBag.writer = writer.NameSurname;
			var blogsbywriter = bm.GetListWithWriter(id).OrderByDescending(x=>x.BlogDate).Take(3);
			return View(blogsbywriter);
		}
	}
}
