using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PresentationLayer.ViewComponents.Writer
{
    public class WriterMessageNotification : ViewComponent
    {
        MessageManager mm = new MessageManager(new EfMessageRepository());
        UserManager um = new UserManager(new EfUserRepository());


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = um.GetByUserName(User.Identity.Name).FirstOrDefault();
            var userId = user.Id;
            var inbox = mm.GetListWithRecieverId(userId).Where(x => x.Status == true).ToList();
            return View(inbox);
        }
    }
}
