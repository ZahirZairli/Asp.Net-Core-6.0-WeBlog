using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Moderator")]
    public class CommentController : Controller
    {
        CommentManager cm = new CommentManager(new EfCommentRepository());
        public IActionResult Index()
        {
            var comments = cm.GetList();
            return View(comments);
        }
        //[HttpDelete]
        public IActionResult Delete(int id)
        {
            var comment = cm.GetById(id);
            cm.TDelete(comment);
            return RedirectToAction("Index");
        }
    }
}
