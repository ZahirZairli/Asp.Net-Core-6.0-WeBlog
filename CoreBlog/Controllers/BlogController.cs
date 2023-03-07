using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace PresentationLayer.Controllers
{
    [AllowAnonymous]
    public class BlogController : Controller
	{
		BlogManager bm = new BlogManager(new EfBlogRepository());
		CategoryManager cm = new CategoryManager(new EfCategoryRepository());
		public IActionResult Index(int? id,string Search,int page=1)
		{
			if (Search != null)
			{
				var blogs = bm.GetListWithCategory().FindAll(x => x.BlogStatus == true && x.BlogContent.Contains(Search)).OrderByDescending(x => x.BlogDate).ToPagedList(page, 6);
				return View(blogs);
			}
			if (id != null)
			{
				var catid = Convert.ToInt32(id);

				var category = cm.GetById(catid);
				ViewBag.categoryname = category.CategoryName;
				var catblog = bm.GetListWithCategory().FindAll(x => x.BlogStatus == true && x.CategoryId == catid).OrderByDescending(x => x.BlogDate).ToPagedList(page, 6);
				return View(catblog);
			}
			else
			{
				var blogs = bm.GetListWithCategory().FindAll(x => x.BlogStatus == true).OrderByDescending(x=>x.BlogDate).ToPagedList(page, 6);
				return View(blogs);
			}
		}
		public IActionResult BlogDetail(int id)
		{
			TempData["message"] = TempData["commentmessage"];

			var blog = bm.GetById(id);
			blog.BlogViewingCount += 1;
			bm.TUpdate(blog);
			return View(blog);
		}
		public IActionResult Like(int id)
		{
			var blog = bm.GetById(id);
			blog.BlogLikeCount += 1;
			bm.TUpdate(blog);
			return RedirectToAction("BlogDetail", "Blog", new { @id = id });
		}
	}
}
