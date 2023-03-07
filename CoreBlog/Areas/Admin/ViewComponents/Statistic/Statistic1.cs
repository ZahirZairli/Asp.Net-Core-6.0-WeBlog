using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace PresentationLayer.Areas.Admin.ViewComponents.Statistic
{
    public class Statistic1 : ViewComponent
    {
        BlogManager bm = new BlogManager(new EfBlogRepository());
        ContactManager cm = new ContactManager(new EfContactRepository());
        CommentManager ctm = new CommentManager(new EfCommentRepository());
        public IViewComponentResult Invoke()
        {
            ViewBag.blogcount = bm.GetListActive().Count();
            ViewBag.contactcount = cm.GetListActive().Count();
            ViewBag.commentcount = ctm.GetListActive().Count();


            string api = "7d25eb467a5e851f9de46d590290c795";
            string city = "baku";
            string connection = "https://api.openweathermap.org/data/2.5/weather?q="+city+"&mode=xml&appid="+api+"&units=metric&lang=az";
            XDocument document = XDocument.Load(connection);
            ViewBag.temperature = document.Descendants("temperature").ElementAt(0).Attribute("value").Value;
            ViewBag.city = document.Descendants("city").ElementAt(0).Attribute("name").Value;


            return View();
        }
    }
}
