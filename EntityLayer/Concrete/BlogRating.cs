using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EntityLayer.Concrete
{
    public class BlogRating
    {
        public int BlogRatingId { get; set; }
        public int BlogId { get; set; }
        public int BlogTotalScore { get; set; }
        public int BlogRatingCount { get; set; }
    }


    ///Trigerlerimiz
    ///1.Bloglarin blog idlerinin buna yazilmasi
    // create Trigger AddBlogInRatingTable
    //on Blogs
    //After Insert
    //As
    //Declare @blogid int
    //Select @blogid = BlogId from inserted
    //Insert into BlogRatings(BlogId, BlogTotalScore, BlogRatingCount)
    //values(@blogid,0,0)
    //bu triggerle bizbloglara yeni blog elave edende onun idsi avtomatik ratingdeki bligid ye dusur

    //sonraki addim ise blogaa puan verdikde bu rating cedveldeki puan avtomatik heminpuanla cmlenmelidir.

    //create Trigger BlogRatingPointInComment
    //on Comments
    //After Insert
    //As
    //Declare @blogid int
    //Declare @point int
    //Select @blogid = BlogId,@point = BlogPoint from inserted
    //Update BlogRatings
    //Set BlogTotalScore+=@point,BlogRatingCount+=1
    //where BlogId = @blogid
}
