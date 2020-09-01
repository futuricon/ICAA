using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ICAA.Models;
using Microsoft.AspNetCore.Mvc;
using ICAA.Models.ViewModels;

namespace ICAA.Controllers
{
    public class NewsController : Controller
    {
        public int PageSize = 4;
        private IBlogRepository repository;
        public NewsController(IBlogRepository repo)
        {
            repository = repo;
        }
        //public ViewResult List() => View(repository.Blogs);

       
        public IActionResult Index(int blogPage = 1) =>
            View(new BlogsListViewModel
            {
                Blogs = repository.Blogs
                .OrderByDescending(p => p.BlogID)
                .Skip((blogPage - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = blogPage,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Blogs.Count()
                }
            });

        
        public IActionResult News(int id)
        {
            var blogModel = new BlogsListViewModel
            {
                Blogs = repository.Blogs.OrderBy(p => p.BlogID).Where(p => p.BlogID == Convert.ToInt16(id))
            };
            return View(blogModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
