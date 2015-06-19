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

        [SiteMapTitle("Title")]
        public ActionResult ListInCate(int id, int? page)
        {
            T_TagServices tagServices = new T_TagServices();
            ViewBag.CateTitle = tagServices.GetByID(id).Title;
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            // Get all with paging
            IEnumerable<T_News> NewsList = _newServices.GetByTaxonomy(id);
            int PageSizeClient;
            Int32.TryParse(TNVCMS.Web.GlobalConfig.Instance.GetValue(TNVCMS.Utilities.Config.PageSizeClient.ToString()), out PageSizeClient);
            PageSizeClient = (PageSizeClient < 1)?20: PageSizeClient;
            IPagedList<T_News> Model = MvcPaging.PagingExtensions.ToPagedList(NewsList, currentPageIndex, PageSizeClient, NewsList.Count());
            return View("ListInCate", Model);

        }
        public PartialViewResult GetLastestNews(int limit = 5)
        {
            //Get limit itme
            IEnumerable<T_News> NewsList = _newServices.GetLastNews(5);
            return PartialView("GetLastestNews", NewsList);

        }

        public PartialViewResult GetRelatedNews(int newsId, int limit = 5)
        {
            //Get limit itme
            IEnumerable<T_News> NewsList = _newServices.GetRelatedNews(newsId, 5);
            return PartialView("GetRelatedNews", NewsList);

        }

        public ActionResult ContentByTag(int id, int? page, int limit = 0)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            int PageSizeClient;
            Int32.TryParse(TNVCMS.Web.GlobalConfig.Instance.GetValue(TNVCMS.Utilities.Config.PageSizeClient.ToString()), out PageSizeClient);
            PageSizeClient = (PageSizeClient < 1) ? 20 : PageSizeClient;

            IT_TagServices tagServices = new T_TagServices();
            T_Tag ThisTag = tagServices.GetByID(id);
            ViewBag.TagTitle = ThisTag.Title;
            //Get limit itme
            IEnumerable<T_News> NewsList = _newServices.GetNewsByTag(id, limit);
            IPagedList<T_News> Model = MvcPaging.PagingExtensions.ToPagedList(NewsList, currentPageIndex, PageSizeClient, NewsList.Count());
            return View("ContentByTag", Model);

        }
    }
}
