using EntityLayer.Concrete;

namespace PresentationLayer.Models
{
    public class AddBlogImage
    {
        public int BlogId { get; set; }
        public int BlogViewingCount { get; set; } = 0;
        public int BlogLikeCount { get; set; } = 0;
        public string BlogTitle { get; set; }
        public string BlogContent { get; set; }
        public IFormFile BlogThumbnailImage { get; set; }
        public IFormFile BlogImage { get; set; }
        public DateTime BlogDate { get; set; } = DateTime.Now;
        public bool BlogStatus { get; set; } = true;
        public int CategoryId { get; set; }
        public int AppUserId { get; set; }
    }
}
