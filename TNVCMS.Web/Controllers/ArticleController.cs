using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcSiteMapProvider.Web.Mvc.Filters;
using TNVCMS.Domain.Model;
using TNVCMS.Domain.Services;

namespace TNVCMS.Web.Controllers
{
    public class ArticleController : Controller
    {
        private IT_NewsServices _newServices;
        private IT_News_TagServices _news_TagServices;
        //
        // GET: /Article/
        public ArticleController()
        {
            _newServices = new T_NewsServices();
            _news_TagServices = new T_News_TagServices();
        }
        public ActionResult Index()
        {
            return View();
        }
        
        [SiteMapTitle("Title")]
        
        public ActionResult Detail(int id, string slug)
        {
            T_News ANews = _newServices.GetByID(id);
            return View(ANews);
        }

        public PartialViewResult GetTags(int id, string taxonomy)
        {
            IEnumerable<T_Tag> TagList = _news_TagServices.GetTagByNewsID(id, taxonomy);
            return PartialView(TagList);
        }

    }
}
