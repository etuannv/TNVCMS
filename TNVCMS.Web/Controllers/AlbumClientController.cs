using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using MvcSiteMapProvider.Web.Mvc.Filters;
using TNVCMS.Domain.Model;
using TNVCMS.Domain.Services;
using TNVCMS.Web.Models;

namespace TNVCMS.Web.Controllers
{
    public class AlbumClientController : Controller
    {
        IT_AlbumServices _service;
        public AlbumClientController()
        {
            _service = new T_AlbumServices();
        }
        //
        // GET: /Album/
        public ActionResult List(int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            IEnumerable<T_Album> albumList = _service.GetAll().OrderByDescending(m=>m.ID);
            int PageSizeClient = Convert.ToInt32(TNVCMS.Web.GlobalConfig.Instance.GetValue(TNVCMS.Utilities.Config.PageSizeAdmin.ToString()));
            PageSizeClient = (PageSizeClient < 1) ? 20 : PageSizeClient;
            IPagedList<T_Album> Model = MvcPaging.PagingExtensions.ToPagedList(albumList, currentPageIndex, PageSizeClient, albumList.Count());
            return View(Model);
        }



        //
        // GET: /Album/
        public ActionResult Detail(int id, int? page)
        {
            T_Album albumList = _service.GetByID(id);
            ViewBag.Title = albumList.Title;
            List<string> FileList = new List<string>();
            T_Album album = _service.GetByID(id);
            var albumFolder = "/Content/Uploads/Album/" + album.Slug;
            if (System.IO.Directory.Exists(Server.MapPath(albumFolder)))
            {
                foreach (var item in System.IO.Directory.GetFiles(Server.MapPath(albumFolder)))
                {
                    FileList.Add(Path.Combine(albumFolder, System.IO.Path.GetFileName(item)));
                }
            }
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            IPagedList<string> MyList = FileList.ToPagedList(currentPageIndex, 20, FileList.Count);

            return View(MyList);
        }

        private List<AlbumViewModel> GetAllAlbum()
        {
            List<AlbumViewModel> ReturnList = new List<AlbumViewModel>();
            string RootPath = Server.MapPath(TNVCMS.Web.GlobalConfig.Instance.GetValue(TNVCMS.Utilities.Config.AlbumPath.ToString()));

            // Get the root directory and print out some information about it.
            System.IO.DirectoryInfo dirInfo = new DirectoryInfo(RootPath);


            System.IO.DirectoryInfo[] dirInfos = dirInfo.GetDirectories("*.*");

            foreach (System.IO.DirectoryInfo d in dirInfos)
            {
                ReturnList.Add(new AlbumViewModel(d.Name, d.FullName));
            }
            return ReturnList;
        }
    }
}
