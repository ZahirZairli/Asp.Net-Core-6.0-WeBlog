using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Moderator")]
    [Area("Admin")]
    public class ChartController : Controller
    {
        CategoryManager cm = new CategoryManager(new EfCategoryRepository());
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CategoryChart()
        {
            var list = cm.GetCategoryChart();
            return Json(new { jsonlist = list });
        }
    }
}
