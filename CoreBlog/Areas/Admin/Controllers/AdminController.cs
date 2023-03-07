using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
        public PartialViewResult AdminLeftMenuPartial()
        {
            return PartialView();
        }
    }
}
