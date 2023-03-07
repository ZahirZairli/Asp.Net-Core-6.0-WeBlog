using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PresentationLayer.Models;
using System.Security.Claims;

namespace PresentationLayer.Controllers
{
    public class WriterPanelBlogController : Controller
    {
        BlogManager bm = new BlogManager(new EfBlogRepository());
        CategoryManager cm = new CategoryManager(new EfCategoryRepository());
        UserManager um = new UserManager(new EfUserRepository());
        public IActionResult MyBlogs()
        {
            var user = um.GetByUserName(User.Identity.Name).FirstOrDefault();   
            var blogs = bm.GetListWithCategoryByWriter(user.Id);
            return View(blogs);
        }
        [HttpGet]
        public IActionResult NewBlog()
        {
            GetListCategories();
            return View();
        }
        [HttpPost]
        public IActionResult NewBlog(AddBlogImage blog)
        {
            Blog newblog = new Blog();



            if (blog.BlogThumbnailImage != null)
            {
                var extension = Path.GetExtension(blog.BlogThumbnailImage.FileName);
                var newimagename = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Web/images/", newimagename);
                var stream = new FileStream(location, FileMode.Create);
                blog.BlogThumbnailImage.CopyTo(stream);
                newblog.BlogThumbnailImage = newimagename;
            }
            else
            {
                newblog.BlogThumbnailImage = "com.png";
            }

            if (blog.BlogImage != null)
            {
                var extension = Path.GetExtension(blog.BlogImage.FileName);
                var newimagename = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Web/images/", newimagename);
                var stream = new FileStream(location, FileMode.Create);
                blog.BlogImage.CopyTo(stream);
                newblog.BlogImage= newimagename;
            }
            else
            {
                newblog.BlogImage = "com.png";
            }
            newblog.BlogViewingCount = 0;
            newblog.BlogLikeCount = 0;
            newblog.BlogTitle = blog.BlogTitle;
            newblog.BlogContent = blog.BlogContent;
            newblog.BlogStatus = blog.BlogStatus;
            newblog.CategoryId = blog.CategoryId;

            BlogValidator bv = new BlogValidator();
            ValidationResult validationResult = bv.Validate(newblog);

            if (validationResult.IsValid)
            {   
                var user = um.GetByUserName(User.Identity.Name).FirstOrDefault();
                newblog.AppUserId = user.Id;
                bm.TAdd(newblog);
                BlogRating br = new BlogRating();
                br.BlogId = newblog.BlogId;
                br.BlogRatingCount = 0;
                br.BlogTotalScore = 0;

                using var c = new Context();
                c.Add(br);
                c.SaveChanges();

                return RedirectToAction("MyBlogs", "WriterPanelBlog");
            }
            else
            {
                GetListCategories();
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            return View();
            }

        }



        public IActionResult Delete(int id)
        {
            var blog = bm.GetById(id);
            bm.TDelete(blog);
            TempData["deletemessage"] = "true";
            return RedirectToAction("MyBlogs");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            GetListCategories();
            var blog = bm.GetById(id);
            return View(blog);
        }

        [HttpPost]
        public IActionResult Edit(AddBlogImage blog)
        {
            var oldblog = bm.GetById(blog.BlogId);
            Blog newblog = new Blog();
            if (blog.BlogImage == null)
            {
                newblog.BlogImage = oldblog.BlogImage;
            }
            else
            {
                var extension = Path.GetExtension(blog.BlogImage.FileName);
                var newimagename = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Web/images/", newimagename);
                var stream = new FileStream(location, FileMode.Create);
                blog.BlogImage.CopyTo(stream);
                newblog.BlogImage = newimagename;
            }
            if (blog.BlogThumbnailImage == null)
            {
                newblog.BlogThumbnailImage = oldblog.BlogThumbnailImage;
            }
            else
            {
                var extension = Path.GetExtension(blog.BlogThumbnailImage.FileName);
                var newimagename = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Web/images/", newimagename);
                var stream = new FileStream(location, FileMode.Create);
                blog.BlogThumbnailImage.CopyTo(stream);
                newblog.BlogThumbnailImage = newimagename;
            }
            newblog.BlogId = blog.BlogId;
            newblog.BlogTitle = blog.BlogTitle;
            newblog.BlogContent = blog.BlogContent;
            newblog.BlogStatus = blog.BlogStatus;
            newblog.CategoryId = blog.CategoryId;
            BlogValidator bv = new BlogValidator();
            ValidationResult validationResult = bv.Validate(newblog);

            if (validationResult.IsValid)
            {
                newblog.BlogLikeCount = oldblog.BlogLikeCount;
                newblog.BlogViewingCount = oldblog.BlogViewingCount;
                newblog.BlogDate = oldblog.BlogDate;
                var user = um.GetByUserName(User.Identity.Name).FirstOrDefault();
                newblog.AppUserId = user.Id;
                bm.TUpdate(newblog);
                return RedirectToAction("MyBlogs", "WriterPanelBlog");
            }
            else
            {
                GetListCategories();
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View();
            }
        }




        public void GetListCategories()
        {
            List<SelectListItem> deger1 = (from x in cm.GetList()
                                           select new SelectListItem
                                           {
                                               Text = x.CategoryName,   //optionun metni
                                               Value = x.CategoryId.ToString() //optionun valuesi
                                           }).ToList();
            ViewBag.dpr = deger1;
        }
    }
}
