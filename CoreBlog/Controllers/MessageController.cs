using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PresentationLayer.Controllers
{
    public class MessageController : Controller
    {
        MessageManager mm = new MessageManager(new EfMessageRepository());
        UserManager um = new UserManager(new EfUserRepository());
        public IActionResult AllInbox()
        {
            var user = um.GetByUserName(User.Identity.Name).FirstOrDefault();
            var userId = user.Id;
            var inbox = mm.GetListWithRecieverId(Convert.ToInt32(userId));
            return View(inbox);
        }
        public IActionResult AllSendBox()
        {
            var user = um.GetByUserName(User.Identity.Name).FirstOrDefault();
            var userId = user.Id;
            var inbox = mm.GetListWithSenderId(Convert.ToInt32(userId));
            return View(inbox);
        }
        public IActionResult Detail(int id)
        {
            var message = mm.GetById(id);

            if (message.Status == true)
            {
                message.Status = false;
                mm.TUpdate(message);
            }
            return View(message);
        }
        public IActionResult Delete(int id)
        {
            var message = mm.GetById(id);
            mm.TDelete(message);
            TempData["deletemessage"] = "true";
            return RedirectToAction("AllInbox","Message");
        }
        [HttpGet]
        public IActionResult NewMessage(int? id)
        {
            if (id != null)
            {
                var user = um.GetById(Convert.ToInt32(id));
                ViewBag.receivermail = user.Email;
            }
            return View();
        }
        [HttpPost]
        public IActionResult NewMessage(Message newmessage,string ReceiverUserMail)
        {
                newmessage.SenderUserId =Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var receiveruser = um.GetByUserMail(ReceiverUserMail).FirstOrDefault();
                if (receiveruser !=null)
                {
                     newmessage.ReceiverUserId = receiveruser.Id;
                     MessageValidator mv = new MessageValidator();
                     ValidationResult result = mv.Validate(newmessage);
                    if (result.IsValid)
                    {
                        mm.TAdd(newmessage);
                        TempData["sendmessage"] = "true";
                        return RedirectToAction("NewMessage","Message");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError(item.PropertyName,item.ErrorMessage);
                        }
                    }
                }
                else
                {
                    TempData["sendmessageerror"] = "true";
                }
                return View();
        }

    }
}
