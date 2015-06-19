using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
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
        public PartialViewResult GetNewsInCategory(int CateId, int Number = 5)
        {
            //Get limit itme
            IEnumerable<T_News> NewsList = _newServices.GetByTaxonomy(CateId, 5);
            return PartialView("GetNewsInCategory", NewsList);
        }

        public ActionResult ListInCate(int id, int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            // Get all with paging
            IEnumerable<T_News> NewsList = _newServices.GetByTaxonomy(id);
            IPagedList<T_News> Model = MvcPaging.PagingExtensions.ToPagedList(NewsList, currentPageIndex, GlobalVariables.PageSize, NewsList.Count());
            return View("ListInCate", Model);

        }

    }
}
