using DocumentFormat.OpenXml.Office2010.Excel;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace PresentationLayer.Controllers
{
    public class NewsLetterApiTestController : Controller
    {
        public async Task<IActionResult>Index()
        {
            var httpClient = new HttpClient();
            var responseMessage = await httpClient.GetAsync("https://localhost:7096/api/NewsLetterApi");
            var jsonstring = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<NewsLetter>>(jsonstring);
            return View(values);
        }
        [HttpGet]
        public IActionResult AddSubscriber()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddSubscriber(NewsLetter newsletter)
        {
            var httpClient = new HttpClient();
            var jsonSubsciber = JsonConvert.SerializeObject(newsletter);
            StringContent content = new StringContent(jsonSubsciber,Encoding.UTF8,"application/json");
            
            var responseMessage = await httpClient.PostAsync("https://localhost:7096/api/NewsLetterApi",content);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["addsubscriber"] = "true";
                return RedirectToAction("Index");
            }
            return View(newsletter);
        }

        [HttpGet]
        public async Task<IActionResult> EditSubscriber(int id)
        {
            var httpClient = new HttpClient();
            var responseMessage = await httpClient.GetAsync("https://localhost:7096/api/NewsLetterApi/"+id);

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonsubscriber= await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<NewsLetter>(jsonsubscriber);
                return View(value);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> EditSubscriber(NewsLetter newsletter)
        {
            var httpClient = new HttpClient();  
            var subscriber = JsonConvert.SerializeObject(newsletter);
            var content = new StringContent(subscriber,Encoding.UTF8,"application/json");
            var responseMessage = await httpClient.PutAsync("https://localhost:7096/api/NewsLetterApi/",content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(newsletter);
            }
        }

        public async Task<IActionResult> DeleteSubscriber (int id)
        {
            var httpClient = new HttpClient();

            var responseMessage = await httpClient.DeleteAsync("https://localhost:7096/api/NewsLetterApi/"+id);

            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["deletesubscriber"] = "true";
            }
            return RedirectToAction("Index");
        }


    }
}
