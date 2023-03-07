    using BusinessLayer.Concrete;
using ClosedXML.Excel;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Moderator")]
    public class BlogController : Controller
    {

        BlogManager bm = new BlogManager(new EfBlogRepository());
        public IActionResult BlogList()
        {
            var blogs = bm.GetListWithCategory();
            return View(blogs);
        }
        public IActionResult ExportExcelBlogList()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Bloq siyahısı");
                worksheet.Cell(1, 1).Value = "Id";
                worksheet.Cell(1, 2).Value = "Başlıq";
                worksheet.Cell(1, 3).Value = "Kateqoriya";
                worksheet.Cell(1, 4).Value = "Bəyəni";
                worksheet.Cell(1, 5).Value = "Baxış";
                worksheet.Cell(1, 6).Value = "Status";

                int BlogRowCount = 2;
                foreach (var item in bm.GetListWithCategory())
                {
                    worksheet.Cell(BlogRowCount, 1).Value = item.BlogId;
                    worksheet.Cell(BlogRowCount, 2).Value = item.BlogTitle;
                    worksheet.Cell(BlogRowCount, 3).Value = item.Category.CategoryName;
                    worksheet.Cell(BlogRowCount, 4).Value = item.BlogLikeCount;
                    worksheet.Cell(BlogRowCount, 5).Value = item.BlogViewingCount;

                    if (item.BlogStatus)
                    {
                        worksheet.Cell(BlogRowCount, 6).Value = "Aktiv";
                    }
                    else
                    {
                        worksheet.Cell(BlogRowCount, 6).Value = "Passiv";
                    }
                    BlogRowCount++;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet", "work1.xlsx");
                }
            }
        }
        public IActionResult BlogListExcel()
        {
            return View();
        }
    }
}
