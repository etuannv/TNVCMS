using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using TNVCMS.Domain.Model;
using TNVCMS.Domain.Services;
using TNVCMS.Web.Models;
using System.Linq;
using System.Text;

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
        //public PartialViewResult GetTopMenu()
        //{
        //    return PartialView("GetTopMenu");
        //}
        public string GetTopMenu()
        {
            T_MenuServices service = new T_MenuServices();
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='navbar'>");
            sb.Append("<button type='button' class='btn btn-navbar-highlight btn-large btn-primary' data-toggle='collapse' data-target='.nav-collapse'>");
            sb.Append("Danh mục <span class='icon-chevron-down icon-white'></span>");
            sb.Append("</button>");
            sb.Append("    <div class='nav-collapse collapse'>");
            sb.Append("        <ul class='nav nav-pills ddmenu'>");
            IEnumerable<T_Menu> Level1 = service.GetChildren(null);
            //Do level 1
            foreach (var item1 in Level1)
            {
                if (service.HasChild(item1.ID))
                {
                    sb.AppendFormat("<li class='dropdown'><a href='{0}' class='dropdown-toggle'>{1} <b class='caret'></b></a>", item1.Link, item1.Title);
                    sb.Append(MakeLevel2(item1.ID));
                    sb.Append("</li>");
                }
                else
                {
                    sb.AppendFormat("<li><a href='{0}'>{1}</a></li>", item1.Link, item1.Title);
                }
            }

            sb.Append(" </ul>");
            sb.Append("      </div>");
            sb.Append("            </div>");
            return sb.ToString();
        }

        private string MakeLevel2(int parentId)
        {
            T_MenuServices service = new T_MenuServices();
            IEnumerable<T_Menu> Level2 = service.GetChildren(parentId);
            StringBuilder sb = new StringBuilder();
            sb.Append("        <ul class='dropdown-menu'>");
            //Do level 1
            foreach (var item2 in Level2)
            {
                if (service.HasChild(item2.ID))
                {
                    sb.AppendFormat("<li><a href='{0}' class='dropdown-toggle'> {1}&nbsp;&raquo;</a>", item2.Link, item2.Title);
                    sb.Append(MakeLevel3(item2.ID));
                    sb.Append("</li>");
                }
                else
                {
                    sb.AppendFormat("<li><a href='{0}'>{1}</a></li>", item2.Link, item2.Title);
                }
            }
            sb.Append("</ul>");
            return sb.ToString();
        }

        private string MakeLevel3(int parentId)
        {
            T_MenuServices service = new T_MenuServices();
            IEnumerable<T_Menu> Level3 = service.GetChildren(parentId);
            StringBuilder sb = new StringBuilder();
            sb.Append("<ul class='dropdown-menu  sub-menu'>");
            //Do level 1
            foreach (var item3 in Level3)
            {
                sb.AppendFormat("<li><a href='{0}'>{1}</a></li>", item3.Link, item3.Title);
            }
            sb.Append("</ul>");
            return sb.ToString();
        }

        public PartialViewResult GetFooter()
        {
            return PartialView("GetFooter");
        }
    }
}
