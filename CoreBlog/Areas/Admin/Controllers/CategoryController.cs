using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace PresentationLayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Moderator")]
    public class CategoryController : Controller
    {
        CategoryManager cm = new CategoryManager(new EfCategoryRepository());
        public IActionResult Index(int page = 1)
        {
            var categories = cm.GetList().ToPagedList(page,5);
            return View(categories);
        }
        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();  
        }
        [HttpPost]
        public IActionResult AddCategory(Category c)
        {
            CategoryValidator cv = new CategoryValidator();
            ValidationResult results = cv.Validate(c);

            if (results.IsValid)
            {
                cm.TAdd(c);
                TempData["addcategory"] = "success";
                return RedirectToAction("Index","Category");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View();
            }
        }
        public IActionResult DeleteCategory(int id)
        {
            var category = cm.GetById(id);
            cm.TDelete(category);
            TempData["deletecategory"] = "true";
            return RedirectToAction("Index");
        }
    }
}
