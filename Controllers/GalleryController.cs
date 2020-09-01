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
    public class GalleryController : Controller
    {
        public int PageSize = 4;
        private IGalleryRepository repositoryG;
        private ITagRepository repositoryT;

        public GalleryController(IGalleryRepository repoG, ITagRepository repoT)
        {
            repositoryG = repoG;
            repositoryT = repoT;
        }



        public IActionResult Index(string tagname, int imgPage = 1)
        {
            if (tagname != null)
            {
                var xx = new GalleryListViewModel();
                var tagData = repositoryT.InfoTags.OrderByDescending(k=>k.ImageID).Where(p => p.TagName == tagname).ToList();
                if (tagData.Count() != 0)
                {
                    //return View(tagdata);
                    ICollection<Gallery> certList = new List<Gallery>();
                    foreach (var item in tagData)
                    {
                        var temp = repositoryG.Images.Where(p => p.ImageID == item.ImageID);
                        foreach (var items in temp)
                        {
                            certList.Add(items);
                        }

                    }
                    var GallTemps = new GalleryListViewModel
                    {
                        Galleries = certList.AsEnumerable()
                                    .Skip((imgPage - 1) * PageSize)
                                    .Take(PageSize),
                        PagingInfo = new PagingInfo
                        {
                            CurrentPage = imgPage,
                            ItemsPerPage = PageSize,
                            TotalItems = tagData.Count()
                        }
                        //Galleries = repositoryG.Images
                        //                .OrderByDescending(p => p.ImageID)
                        //                .Where(t => t.InfoTags.Where(x => x.TagName.Contains(tagName)))
                    };
                    ViewData["tagName"] = tagname;
                    return View(GallTemps);
                }
                else
                {
                    var GallTemp = new GalleryListViewModel
                    {
                        Galleries = repositoryG.Images
                                        .OrderByDescending(p => p.ImageID)
                                        .Skip((imgPage - 1) * PageSize)
                                        .Take(PageSize),
                        PagingInfo = new PagingInfo
                        {
                            CurrentPage = imgPage,
                            ItemsPerPage = PageSize,
                            TotalItems = repositoryG.Images.Count()
                        }
                    };
                    return View(GallTemp);
                }
            }
            else
            {
                var GallTemp = new GalleryListViewModel
                {
                    Galleries =repositoryG.Images
                                    .OrderByDescending(p => p.ImageID)
                                    .Skip((imgPage - 1) * PageSize)
                                    .Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = imgPage,
                        ItemsPerPage = PageSize,
                        TotalItems = repositoryG.Images.Count()
                    }
                };
                return View(GallTemp);
            }
            
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
