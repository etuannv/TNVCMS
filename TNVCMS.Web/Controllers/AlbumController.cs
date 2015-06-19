using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcSiteMapProvider.Web.Mvc.Filters;
using TNVCMS.Domain.Model;
using TNVCMS.Domain.Services;
using TNVCMS.Web.Models;

namespace TNVCMS.Web.Controllers
{
    public class AlbumController : Controller
    {
        public AlbumController()
        {
        }
        //
        // GET: /Album/
        public ActionResult List()
        {
            List<AlbumViewModel> albumList = GetAllAlbum();
            return View(albumList);
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
