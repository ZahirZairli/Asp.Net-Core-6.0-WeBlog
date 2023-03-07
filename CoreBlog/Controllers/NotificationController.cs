using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    public class NotificationController : Controller
    {
        NotificationManager nm = new NotificationManager(new EfNotificationRepository());
        public IActionResult AllNotifications()
        {
            var notifies = nm.GetList();
            return View(notifies);
        }
        public IActionResult Detail(int id)
        {
            var notifies = nm.GetById(id);
            notifies.NotificationStatus = false;
            nm.TUpdate(notifies);
            return View(notifies);
        }
    }
}
