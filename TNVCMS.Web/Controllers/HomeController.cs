using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using TNVCMS.Domain.Model;
using TNVCMS.Domain.Services;
using TNVCMS.Web.Models;

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
            IEnumerable<T_Slide> slideImages = _slideServices.GetEnableSlide();
            return PartialView("GetSlide", slideImages);
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
