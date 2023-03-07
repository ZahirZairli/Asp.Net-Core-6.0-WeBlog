using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace PresentationLayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Moderator")]
    public class WriterController : Controller
    {
        WriterManager wm = new WriterManager(new EfWriterRepository());
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetWriters()
        {
            var writers = wm.GetList();
            var jsonwriters = JsonConvert.SerializeObject(writers);
            return Json(jsonwriters);   
        }
        public IActionResult GetWriterById(int writerid)
        {
            var writer = wm.GetById(writerid);
            var jsonwriter = JsonConvert.SerializeObject(writer);
            return Json(jsonwriter);    
        }
        public IActionResult WriterDelete(int writerid)
        {
            var writer = wm.GetById(writerid);
            wm.TDelete(writer);
            var jsonwriter = JsonConvert.SerializeObject(writer);
            return Json(jsonwriter);
        }

        public IActionResult UpdateWriter(Writer w)
        {
            w.WriterImage = "com.png";
            w.WriterRoleId = 0;
            w.WriterPassword2 = w.WriterPassword;
            w.WriterCity = "Bakı";
            wm.TUpdate(w);
            var jsonwriter = JsonConvert.SerializeObject(w);
            return Json(jsonwriter);
        }
        [HttpPost]
        public IActionResult AddWriter(Writer writer)
        {
            writer.WriterImage = "com.png";
            writer.WriterRoleId = 0;
            writer.WriterPassword2 = writer.WriterPassword;
            writer.WriterCity = "Bakı";
            wm.TAdd(writer);
            var jsonWriters = JsonConvert.SerializeObject(writer);
            return Json(jsonWriters);
        }
    }
}
