using DataAccessLayer.Concrete;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
//Identityden istifade ucun
builder.Services.AddDbContext<Context>();
builder.Services.AddIdentity<AppUser, AppRole>(x =>
{
    x.Password.RequireNonAlphanumeric = false;
    x.Password.RequireUppercase = false;    
}).AddEntityFrameworkStores<Context>();

builder.Services.AddControllersWithViews();
//Authorzie islemleri start

//authorize 1 ci addim
builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});
//authorize 2 ci addim
builder.Services.AddSession();
//Authorzie islemleri end (altda 3 cu addim var app.UseAuthentication(); olan)
//authorize 4 cu addim return url login olmayanlar ucun
builder.Services.AddMvc();
builder.Services.AddAuthentication(
        CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(x =>
    {
        x.LoginPath = "/Login/SignIn";
    }
    );
// 4cu addim bitdi
//5ci
builder.Services.ConfigureApplicationCookie(opts =>
{
    //Cookie settings
    opts.Cookie.HttpOnly = true;
    opts.ExpireTimeSpan = TimeSpan.FromMinutes(100);
    opts.AccessDeniedPath = new PathString("/Login/AccessDenied/");
    opts.LoginPath = "/Login/SignIn/";
    opts.SlidingExpiration = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//Error sehifesi yaratmaq start
//app.UseStatusCodePages();
//Error sehifesi yaratmaq end bu biize errorun kodun gosterir saytda
//ozumuz page yaratmaq isteyirikse ve xetalarin novune gore o sehifeye yonlendirmek etmek isteyirikse alttaki kimi yaziriq
app.UseStatusCodePagesWithReExecute("/ErrorPage/Error1", "?code={0}");
//burada yonlendirdiyimiz sehife ErrorPage kontunn icindeki error1 dir. Error kodu ise code nin icerisindekidir.code ile kontrollerdeki ad 
//eyni olmalidir


///Authorize 3 addim
app.UseAuthentication();


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
  name: "areas",
  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Blog}/{action=Index}/{id?}");

app.Run();








//CookieBuilder cookieBuilder = New CookieBuilder();

//IServiceCollection serviceCollection = services.ConfigureApplicationCookie(opts =>
//{
//    opts.Cookie = cookrBuilder;
//    opts => ExpireTimeSpan = System.TipeSpan.FromDays(1);
//});