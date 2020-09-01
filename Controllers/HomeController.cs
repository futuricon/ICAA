using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ICAA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using ICAA.Models.ViewModels;

namespace ICAA.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStringLocalizer<HomeController> _localizer;

        //public HomeController(IStringLocalizer<HomeController> localizer)
        //{
        //    _localizer = localizer;
        //}
        private IStaffRepository repository;
        private IGalleryRepository repositoryG;
        public HomeController(IStringLocalizer<HomeController> localizer, IStaffRepository repo, IGalleryRepository repoG)
        {
            _localizer = localizer;
            repository = repo;
            repositoryG = repoG;
        }

        
        public IActionResult Index() =>
            View(new StaffListViewModel
            {
                Staffs = repository.Staffs
                .OrderBy(p => p.StaffID),
                Galleries = repositoryG.Images.OrderByDescending(k => k.ImageID).Where(p => p.ImagePath.Contains("-GImg.jpg")).Take(10)
            });


        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            //this.HttpContext.Response.Cookies.Append
            //(
            //    CookieRequestCultureProvider.DefaultCookieName,
            //    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture))
            //);
            //return new RedirectResult("~/");
            //Response.Cookies.Append("rudeCookie", "I don't need no user to tell me it's ok.", options);

            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            //var langCookie = Request.Cookies[".AspNetCore.Culture"];

            return LocalRedirect(returnUrl);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
