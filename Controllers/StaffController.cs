using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ICAA.Models;
using ICAA.Models.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ICAA.Controllers
{
    public class StaffController : Controller
    {
        //public int PageSize = 4;
        private IStaffRepository repository;
        private ICertRepository repositoryCert;
        public StaffController(IStaffRepository repo, ICertRepository repoC)
        {
            repositoryCert = repoC;
            repository = repo;
        }
        
        public IActionResult Index(int id)
        {
            //personId = 3;
            //var certList = new CertListViewModel();
            //certList.Certificates = repositoryCert.Certificates
            //    .OrderBy(p => p.StaffId)
            //    .Where(k => k.StaffId == Convert.ToInt16(xStaffId));
            var indexModel = new StaffListViewModel();
            indexModel.Staffs = repository.Staffs
                .OrderBy(p => p.StaffID)
                .Where(p => p.StaffID == Convert.ToInt16(id));
            return View(indexModel);
        }



        //public IActionResult StaffPage(int personId) =>
        //    View(new IndexListViewModel
        //    {
        //        Staffs = repository.Staffs
        //        .OrderBy(p => p.StaffID).Where(p=> p.StaffID == personId),
        //        PesonId = personId.ToString()
        //    });

        // GET: /<controller>/
        //public IActionResult Staff(int satffPage = 1) =>
        //    View(new IndexListViewModel
        //    {
        //        Staffs = repository.Staffs
        //        .OrderBy(p => p.StaffID)
        //        .Skip((satffPage - 1) * PageSize)
        //        .Take(PageSize),
        //        StaffPagingInfo = new StaffPagingInfo
        //        {
        //            CurrentPage = satffPage,
        //            ItemsPerPage = PageSize,
        //            TotalItems = repository.Staffs.Count()
        //        }
        //    });

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
