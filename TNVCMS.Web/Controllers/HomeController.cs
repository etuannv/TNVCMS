using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using TNVCMS.Domain.Model;
using TNVCMS.Domain.Services;
using TNVCMS.Web.Models;
using System.Linq;

namespace TNVCMS.Web.Controllers
{
    public class HomeController : Controller
    {
        private IT_NewsServices _newsServices;
        private IT_SlideServices _slideServices;

        public HomeController()
        {
            _newsServices = new T_NewsServices();
            _slideServices = new T_SlideServices();

        }

        public ActionResult Index()
        {
            ViewData["hasSlide"] = true;
            return View();
        }

        public PartialViewResult GetSlide()
        {
            int SlideGroupID = 0;
            int.TryParse(GlobalConfig.Instance.GetValue(Utilities.Config.SlideChinhGroupID.ToString()), out SlideGroupID);
            IEnumerable<T_Slide> slideImages = _slideServices.GetSlideByGroupID(SlideGroupID);
            return PartialView("GetSlide", slideImages);
        }
        public PartialViewResult GetCarousel()
        {
            int SlideGroupID = 0;
            int.TryParse(GlobalConfig.Instance.GetValue(Utilities.Config.SlideDoiTacGroupID.ToString()), out SlideGroupID);
            IEnumerable<T_Slide> slideImages = _slideServices.GetSlideByGroupID(SlideGroupID);
            return PartialView("GetCarousel", slideImages.ToList());
        }

        public PartialViewResult GetSlideOne(int id, string width = "200px", string height = "200px")
        {
            ViewBag.Subfix = TNVCMS.Utilities.Common.GetUniqueString();
            ViewBag.SlideWidth = width;
            ViewBag.SlideHeight = height;
            IEnumerable<T_Slide> slideImages = _slideServices.GetSlideByGroupID(id);
            return PartialView("GetSlideOne", slideImages.ToList());
        }

        public PartialViewResult GetTopMenu()
        {
            return PartialView("GetTopMenu");
        }

        public PartialViewResult GetFooter()
        {
            return PartialView("GetFooter");
        }
    }
}
