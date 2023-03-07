using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.ViewComponents.Comment
{
    public class AddComment : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
	}
}
