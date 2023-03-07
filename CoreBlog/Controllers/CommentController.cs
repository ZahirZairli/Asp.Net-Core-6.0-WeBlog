using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers
{
    public class CommentController : Controller
	{
		CommentManager cm = new CommentManager(new EfCommentRepository());

		[AllowAnonymous]
		public PartialViewResult PartialAddComment()
		{
			return PartialView();
		}
		[AllowAnonymous]
		public PartialViewResult PartialCommentListByBlog(int id)
		{
			var comments = cm.GetListByBlog(id);
			return PartialView(comments);
		}

		[HttpPost]
		public IActionResult AddComment(Comment c)
		{
			if (c.BlogPoint == null)
			{
				c.BlogPoint = 0;
			}
			cm.TAdd(c);
            using var con = new Context();
			var ratingblog = con.BlogRatings.Where(x => x.BlogId == c.BlogId).FirstOrDefault();
			ratingblog.BlogRatingCount += 1;
			ratingblog.BlogTotalScore += c.BlogPoint;
            con.Update(ratingblog);
            con.SaveChanges();
            TempData["commentmessage"] = "Uğurla yorum yazıldı";
			return RedirectToAction("BlogDetail", "Blog", new { id = c.BlogId });
		}
	}
}
