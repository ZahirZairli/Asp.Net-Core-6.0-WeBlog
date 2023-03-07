using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class Context : IdentityDbContext<AppUser, AppRole, int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
             optionsBuilder.UseSqlServer("workstation id=zahircoreblogdb.mssql.somee.com;packet size=4096;user id=zairovfarid_SQLLogin_1;pwd=66rmt3mdnd;data source=zahircoreblogdb.mssql.somee.com;persist security info=False;initial catalog=zahircoreblogdb;TrustServerCertificate=True;");
            //optionsBuilder.UseSqlServer("Server=DESKTOP-SDVG61S\\SQLEXPRESS;database=CoreBlogDb;" +
            //    "integrated security=true;TrustServerCertificate=True");
        }

        ///1 ə çox əlaqə(mesjnan istifadəçi cədvəli arasında)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>()
                .HasOne(x => x.UserReceiverUser)
                .WithMany(y => y.UserReceiver)
                .HasForeignKey(z => z.ReceiverUserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Message>()
                .HasOne(x => x.UserSenderUser)
                .WithMany(y => y.UserSender)
                .HasForeignKey(z => z.SenderUserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            //identity yuklenen zaman 1e cox elaqede error verir oa gore biz base i yazdiq ki xeta aradan qalxsin
            base.OnModelCreating(modelBuilder);
        }
        ///
        public DbSet<About> Abouts { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Writer> Writers { get; set; }
        public DbSet<BlogRating> BlogRatings { get; set; }
        public DbSet<NewsLetter> NewsLetters { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}
