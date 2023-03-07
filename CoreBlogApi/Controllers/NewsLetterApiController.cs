using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreBlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsLetterApiController : ControllerBase
    {
        NewsLetterManager nm = new NewsLetterManager(new EfNewsLetterRepository());
        [HttpGet]
        public IActionResult SubscriberList()
        {
            var subscribers = nm.GetList();
            return Ok(subscribers);
        }
        [HttpPost]
        public IActionResult SubscriberAdd(NewsLetter c)
        {
            nm.TAdd(c);
            return Ok();
        }
        [HttpGet("{id}")]
        public IActionResult GetSubscriberById(int id)
        {
            var subscriber = nm.GetById(id);

            if (subscriber != null)
            {
                return Ok(subscriber);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpDelete("{id}")]
        public IActionResult SubscribeDelete(int id)
        {
            var subscriber = nm.GetById(id);
            if (subscriber != null)
            {
                nm.TDelete(subscriber);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut]
        public IActionResult SubscriberUpdate(NewsLetter newsletter)
        {
            var subscriber = nm.GetById(newsletter.MailId);

            if (subscriber != null)
            {
                nm.TUpdate(newsletter);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
