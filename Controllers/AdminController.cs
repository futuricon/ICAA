using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using ICAA.Models.ViewModels;
using System;
using SmartBreadcrumbs.Attributes;
using ICAA.Models;

namespace ICAA.Controllers
{
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class AdminController : Controller
    {
        private IBlogRepository repositoryBlog;
        private IStaffRepository repositoryStaff;
        private ICertRepository repositoryCert;
        private IGalleryRepository repositoryGall;
        private ITagRepository repositoryTag;
        IHostingEnvironment _appEnv;
        public AdminController(IHostingEnvironment env, 
            IBlogRepository repoB, 
            IStaffRepository repoS, 
            ICertRepository repoC,
            IGalleryRepository repoG,
            ITagRepository repoT)
        {
            _appEnv = env;
            repositoryBlog = repoB;
            repositoryStaff = repoS;
            repositoryCert = repoC;
            repositoryGall = repoG;
            repositoryTag = repoT;
        }


        [DefaultBreadcrumb("Admin menu")]
        public IActionResult Index() => View();

        [Breadcrumb("Staffs List", FromAction = "Index")]
        public ViewResult StaffsList() =>
            View(repositoryStaff.Staffs);

        [Breadcrumb("Blogs List", FromAction = "Index")]
        public ViewResult BlogsList() =>
            View(repositoryBlog.Blogs);

        [Breadcrumb("Gallery List", FromAction = "Index")]
        public ViewResult GalleryList() =>
            View(repositoryGall.Images);


        //---------------vvv---Blog List---vvv---------------
        #region Blog List

        [Breadcrumb("ViewData.Title", FromAction = "BlogsList")]
        public ViewResult EditBlog(int blogId) =>
            View(repositoryBlog.Blogs.FirstOrDefault(p => p.BlogID == blogId));

        [Breadcrumb("ViewData.Title", FromAction = "BlogsList")]
        [HttpPost]
        public async Task<IActionResult> EditBlog(Blog blog, IFormFile uploadedFile)
        {
            System.Random rand = new System.Random();
            if (ModelState.IsValid)
            {
                if(uploadedFile != null)
                {
                    
                    // путь к папке Files
                    string path;
                    if (blog.BlogID == 0)
                    {
                        if (repositoryBlog.Blogs.Count() != 0)
                        {
                            path = "/db_data/Blog/TitleImg/" + repositoryBlog.Blogs.LastOrDefault().BlogID.ToString() + rand.Next(0, 99) + "-BlogImg.jpg";
                        }
                        path = "/db_data/Blog/TitleImg/" + 1 + rand.Next(0, 99) + "-BlogImg.jpg";
                    }
                    else
                    {
                        path = "/db_data/Blog/TitleImg/" + blog.BlogID + rand.Next(0, 99) + "-BlogImg.jpg";
                        //string untrustedFileName = Path.GetFileName(path);
                    }
                    // сохраняем файл в папку Files в каталоге wwwroot
                    using (var fileStream = new FileStream(_appEnv.WebRootPath + path, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(fileStream);
                    }
                    blog.ImageUrl = path;
                    repositoryBlog.SaveBlog(blog);
                    TempData["message"] = $"{blog.Title} has been saved";
                    return RedirectToAction("BlogsList");
                }
                else if(blog.BlogID > 0){
                    repositoryBlog.SaveBlog(blog);
                    TempData["message"] = $"{blog.Title} has been saved";
                    return RedirectToAction("BlogsList");
                }
                else {
                    // there is something wrong with the data values                
                    return View(blog);
                }
            }
            else{
                // there is something wrong with the data values                
                return View(blog);
            }
        }

        [Breadcrumb("ViewData.Title", FromAction = "BlogsList")]
        public ViewResult CreateBlog()
        {
            return View("EditBlog", new Blog());
        }

        [HttpPost]
        public IActionResult DeleteBlog(int blogId)
        {
            foreach (var item in repositoryBlog.Blogs.Where(p => p.BlogID == blogId))
            {
                if (System.IO.File.Exists(_appEnv.WebRootPath + item.ImageUrl))
                {
                    System.IO.File.Delete(_appEnv.WebRootPath + item.ImageUrl);
                }
            }
            Blog deletedBlog = repositoryBlog.DeleteBlog(blogId);
            if (deletedBlog != null)
            {
                TempData["message"] = $"{deletedBlog.Title} was deleted";
            }
            return RedirectToAction("BlogsList");
        }

        #endregion

        //---------------vvv---Certificate List---vvv---------------
        #region Certificate List
        [Breadcrumb("ViewData.Title", FromAction = "StaffsList")]
        public ViewResult EditCert(int staffId)
        {
            ViewData["StaffID"] = staffId;
            var CertTemp = new CertListViewModel {Certificates =
                repositoryCert.Certificates
                .OrderByDescending(p => p.CertID)
                .Where(k => k.StaffId == staffId)
            };
            return View(CertTemp);
        }
            //View(new CertListViewModel
            //{
            //    Certificates = repositoryCert.Certificates
            //    .OrderByDescending(p => p.CertID).Where(k => k.StaffId == staffId)
            //});

        //ViewData["Message"] = "Hello ASP.NET Core";

        [HttpPost]
        public IActionResult DeleteCert(int certId, string certPath)
        {
            
            if (System.IO.File.Exists(_appEnv.WebRootPath + certPath))
            {
                System.IO.File.Delete(_appEnv.WebRootPath + certPath);
            }
            Certificate deleteCert = repositoryCert.DeleteCert(certId);
            if (deleteCert != null)
            {
                TempData["message"] = $"{deleteCert.CertName} was deleted";
            }
            return RedirectToAction("EditCert", new { staffId = deleteCert.StaffId });
        }

        public async Task<IActionResult> AddCert(string staffId, IList<IFormFile> upFileCert)
        {
            int sId = Convert.ToInt16(staffId);
            if (upFileCert != null && upFileCert.Count() != 0)
            {
                foreach (var file in upFileCert)
                {
                    if (file.Length > 0)
                    {
                        var cert = new Certificate();
                        string filePath = "/db_data/Staff/Certificates/" + staffId + file.FileName + "-CImg.jpg";
                        using (var fileStream = new FileStream(_appEnv.WebRootPath + filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                        cert.CertPath = filePath;
                        cert.StaffId = sId;
                        repositoryCert.SaveCert(cert);
                    }
                }
                if (upFileCert.Count() > 1){
                    TempData["message"] = $"{upFileCert.Count()} files was added";
                }
                else{ TempData["message"] = "file was added"; }
                return RedirectToAction("EditCert", new { staffId = sId });
            }
            else
            {
                TempData["alert"] = "Choose Photo!";
                return RedirectToAction("EditCert", new { staffId = sId });
            }
        }
        public IActionResult AddCertName(Certificate cert, int certId, int staffId)
        {
            cert.CertID = certId;
            cert.StaffId = staffId;
            repositoryCert.SaveCert(cert);
            TempData["message"] = "Done!";
            return RedirectToAction("EditCert", new { staffId = cert.StaffId });
        }
        #endregion

        //---------------vvv---Staff List---vvv---------------
        #region Staff List
        [Breadcrumb("ViewData.Title", FromAction = "StaffsList")]
        public ViewResult EditStaff(int staffId) =>
            View(repositoryStaff.Staffs.FirstOrDefault(p => p.StaffID == staffId));

        public ActionResult NewWindow(string strr)
        {
            return Content(strr);
        }

        [Breadcrumb("ViewData.Title", FromAction = "StaffsList")]
        [HttpPost]
        public async Task<IActionResult> EditStaff(Staff staff, IFormFile uploadedFile, IList<IFormFile> upFileCert)
        {
            NewWindow(uploadedFile.FileName.ToString());
            System.Random rand = new System.Random();
            if (ModelState.IsValid)
            {
                NewWindow("ModelState.IsValid");

                if (upFileCert.Count() != 0)
                {
                    NewWindow("upFileCert.Count() != 0" + upFileCert.Count().ToString());
                    ICollection<Certificate> certList = new List<Certificate>();
                    
                    var uploads = Path.Combine();
                    foreach (var file in upFileCert)
                    {
                        if (file.Length > 0)
                        {
                            var cert = new Certificate();
                            string filePath = "/db_data/Staff/Certificates/" + staff.StaffID + file.FileName + "-CImg.jpg";
                            using (var fileStream = new FileStream(_appEnv.WebRootPath + filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                            }
                            cert.CertPath = filePath;
                            certList.Add(cert);
                        }
                        NewWindow("savedAllCerts");
                    }
                    staff.Certificates = certList;
                    repositoryStaff.SaveStaff(staff);
                    NewWindow("repositoryStaff.SaveStaff(staff);");
                }
                if (uploadedFile != null)
                {
                    NewWindow("uploadedFile != null");
                    // путь к папке Files
                    string path;
                    
                    if (staff.StaffID == 0)
                    {
                        if (repositoryStaff.Staffs.Count() != 0)
                        {
                            path = "/db_data/Staff/PersonImg/" + repositoryStaff.Staffs.LastOrDefault().StaffID.ToString() + uploadedFile.FileName + "-SImg.jpg";
                        }
                        path = "/db_data/Staff/PersonImg/" + 1 + uploadedFile.FileName + "-SImg.jpg";
                    }
                    else
                    {
                        path = "/db_data/Staff/PersonImg/" + staff.StaffID +"-SImg.jpg";
                        //string untrustedFileName = Path.GetFileName(path);
                    }
                    // сохраняем файл в папку в каталоге wwwroot
                    using (var fileStream = new FileStream(_appEnv.WebRootPath + path, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(fileStream);
                    }
                    staff.ImageUrl = path;
                    
                    repositoryStaff.SaveStaff(staff);
                    TempData["message"] = $"{staff.Name} {staff.Surname} has been saved";
                    return RedirectToAction("StaffsList");
                }
                else if (staff.StaffID > 0)
                {
                    repositoryStaff.SaveStaff(staff);
                    TempData["message"] = $"{staff.Name} {staff.Surname} has been saved";
                    return RedirectToAction("StaffsList");
                }
                else
                {
                    // there is something wrong with the data values                
                    return View(staff);
                }
            }
            else
            {
                // there is something wrong with the data values                
                return View(staff);
            }
        }

        [Breadcrumb("ViewData.Title", FromAction = "StaffsList")]
        public ViewResult CreateStaff() => View("EditStaff", new Staff());

        [HttpPost]
        public IActionResult DeleteStaff(int staffId)
        {

            foreach (var item in repositoryStaff.Staffs.Where(k => k.StaffID == staffId))
            {
                
                if (System.IO.File.Exists(_appEnv.WebRootPath + item.ImageUrl))
                {
                    System.IO.File.Delete(_appEnv.WebRootPath + item.ImageUrl);
                }
                foreach (var SubItem in item.Certificates)
                {
                    if (System.IO.File.Exists(_appEnv.WebRootPath + SubItem.CertPath))
                    {
                        System.IO.File.Delete(_appEnv.WebRootPath + SubItem.CertPath);
                    }
                }
            }
            Staff deletedStaff = repositoryStaff.DeleteStaff(staffId);

            if (deletedStaff != null)
            {
                TempData["message"] = $"{deletedStaff.Name} {deletedStaff.Surname} was deleted";
            }
            return RedirectToAction("StaffsList");
        }
        #endregion

        //---------------vvv---Gallery List---vvv---------------
        #region Gallery List
        [Breadcrumb("ViewData.Title", FromAction = "GalleryList")]
        [HttpPost]
        public async Task<IActionResult> AddImage(IFormFile uploadFile, string info)
        {
            if (uploadFile != null)
            {
                Gallery gall = new Gallery();
                // путь к папке Files
                string path = "/db_data/Gallery/" + uploadFile.FileName + "-GImg.jpg";
                using (var fileStream = new FileStream(_appEnv.WebRootPath + path, FileMode.Create))
                {
                    await uploadFile.CopyToAsync(fileStream);
                }
                if (info != null)
                {
                    ICollection<InfoTag> tagList = new List<InfoTag>();
                    string[] arr = info.Split("#");
                    for (int i = 1; i < arr.Length; i++)
                    {
                        var tag = new InfoTag
                        {
                            TagName = arr[i].ToLower()
                        };
                        tagList.Add(tag);
                    }
                    gall.InfoTags = tagList;
                    gall.Info = info;
                    gall.ImagePath = path;
                    gall.UploadDate = DateTime.Now;
                    repositoryGall.SaveImage(gall);
                    TempData["message"] = $"{uploadFile.FileName} has been saved";
                    return RedirectToAction("GalleryList");
                }
                else
                {
                    TempData["alert"] = "add some tags!";
                    return RedirectToAction("GalleryList");
                }
                
            }
            else
            {
                TempData["alert"] = "choose image!";
                return RedirectToAction("GalleryList");
            }
        }

        [HttpPost]
        public IActionResult AddVideo(string videoPath, string info)
        {
            if (videoPath != null && info != null)
            {
                string tempUrl = videoPath.Remove(0, 17);
                tempUrl = tempUrl.Insert(0, "https://www.youtube.com/embed/");
                Gallery gall = new Gallery();
                ICollection<InfoTag> tagList = new List<InfoTag>();
                string[] arr = info.Split("#");
                for (int i = 1; i < arr.Length; i++)
                {
                    var tag = new InfoTag
                    {
                        TagName = arr[i].ToLower()
                    };
                    tagList.Add(tag);
                }
                gall.InfoTags = tagList;
                gall.Info = info;
                gall.ImagePath = tempUrl;
                gall.UploadDate = DateTime.Now;
                repositoryGall.SaveImage(gall);
                TempData["message"] = $"{videoPath} has been saved";
                return RedirectToAction("GalleryList");
            }
            else
            {
                if (info == null)
                {
                    TempData["alert"] = "add some tags!";
                    return RedirectToAction("GalleryList");
                }
                TempData["alert"] = "add video url!";
                return RedirectToAction("GalleryList");
            }
        }

        public IActionResult DeleteImage(int id)
        {
            foreach (var item in repositoryGall.Images.Where(k => k.ImageID == id))
            {

                if (System.IO.File.Exists(_appEnv.WebRootPath + item.ImagePath))
                {
                    System.IO.File.Delete(_appEnv.WebRootPath + item.ImagePath);
                }
            }
            Gallery deletedImage = repositoryGall.DeleteImage(id);

            if (deletedImage != null)
            {
                TempData["message"] = $"{deletedImage.Info}  was deleted";
            }
            return RedirectToAction("GalleryList");
        }

        [HttpPost]
        public IActionResult AddInfoTag(Gallery gall, string Tag)
        {
            if (ModelState.IsValid)
            {
                if (Tag != null)
                {
                    ICollection<InfoTag> tagList = new List<InfoTag>();
                    string[] arr = Tag.Split("#");
                    for (int i = 1; i < arr.Length; i++)
                    {
                        var cert = new InfoTag
                        {
                            TagName = arr[i]
                        };
                        tagList.Add(cert);
                    }
                    gall.InfoTags = tagList;
                    repositoryGall.SaveImage(gall);
                }
            }
            return RedirectToAction("GalleryList");
        }
        #endregion

        //---------------vvv---Tag---vvv---------------
        #region Tag
        [HttpPost]
        public IActionResult ChangeInfoTag(string imageId, string TagList)
        {
            if (ModelState.IsValid)
            {
                if (TagList != null)
                {
                    var listOfTags = repositoryTag.InfoTags.Where(p => p.ImageID == Convert.ToInt32(imageId)).ToList();
                    foreach (var item in listOfTags)
                    {
                        repositoryTag.DeleteTag(item.id);
                    }
                    var imgInfo = repositoryGall.Images.Where(t => t.ImageID == Convert.ToInt32(imageId));
                    
                    foreach (var item in imgInfo)
                    {
                        item.Info = TagList;
                    }
                    string[] arr = TagList.Split("#");
                    for (int i = 1; i < arr.Length; i++)
                    {
                        var tag = new InfoTag
                        {
                            TagName = arr[i].ToLower(),
                            ImageID = Convert.ToInt32(imageId)
                        };
                        repositoryTag.SaveTag(tag);
                    }
                    TempData["message"] = " tag was changed";
                }
            }
            return RedirectToAction("GalleryList");
        }
        #endregion
    }
}

