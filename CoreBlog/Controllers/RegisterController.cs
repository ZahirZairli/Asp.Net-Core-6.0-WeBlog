using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        WriterManager wm = new WriterManager(new EfWriterRepository());
        [HttpGet]
        public IActionResult SignUp()
        {
            GetListCities();
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(Writer writer)
        {
            WriterValidator wv = new WriterValidator();
            ValidationResult results = wv.Validate(writer);

            var writers = wm.GetListByMail(writer.WriterMail);
            if (writers.Count == 0)
            {
                if (results.IsValid)
                {

                    writer.WriterRoleId = 0;
                    writer.WriterImage = "com.png";
                    wm.TAdd(writer);
                    TempData["message"] = "succes";
                    return RedirectToAction("Index", "Blog");
                }
                else
                {
                    GetListCities();
                    TempData["errormessage"] = "error";
                    foreach (var item in results.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                }
                return View();
            }
            else
            {
                GetListCities();
                TempData["errormessagemail"] = "error";
                return View();
            }

        }

        public void GetListCities()
        {
            City c = new City();
            List<SelectListItem> deger1 = (from x in c.RegisterCities()
                                           select new SelectListItem
                                           {
                                               Text = x.CityName,   //optionun metni
                                               Value = x.CityName //optionun valuesi
                                           }).ToList();
            ViewBag.dpr = deger1;
        }
    }
}
