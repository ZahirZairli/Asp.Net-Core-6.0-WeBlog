using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.ViewComponents.Blog
{
    public class BlogByWriterMostActive : ViewComponent
    {
        BlogManager bm = new BlogManager(new EfBlogRepository());
        WriterManager wm = new WriterManager(new EfWriterRepository());
        UserManager um = new UserManager(new EfUserRepository());
		private readonly UserManager<AppUser> _userManager;

		public BlogByWriterMostActive(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task<IViewComponentResult> InvokeAsync()
        {
            var user= await _userManager.FindByNameAsync(User.Identity.Name);
			var blogsbywriter = bm.GetListWithWriter(user.Id).OrderByDescending(x => x.BlogViewingCount).Take(4).ToList();
            ViewBag.writername = user.NameSurname;
            ViewBag.writerimage = user.ImageUrl;
            ViewBag.writerabout ="Telefon: " + user.PhoneNumber +" " + "Mail: "+ user.Email;
            ViewBag.writermail = user.Email;
            return View(blogsbywriter);
        }
    }
}
