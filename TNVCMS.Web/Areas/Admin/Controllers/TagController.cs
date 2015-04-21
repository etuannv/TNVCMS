using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using TNVCMS.Data.DatabaseModel;
using TNVCMS.Domain.Services;
using TNVCMS.Web.Areas.Admin.Models;

namespace TNVCMS.Web.Areas.Admin.Controllers
{
    public class TagController : Controller
    {
        private readonly IT_TagServices _tagServices;

        public TagController(IT_TagServices tagServices)
        {
            _tagServices = tagServices;
        }
        //
        // GET: /Admin/Tag/

        public ActionResult List(string taxonomy, string search, int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            ViewData["taxonomy"] = taxonomy;
            ViewData["search"] = search;
            IEnumerable<T_Tag> Cate = _tagServices.GetByTaxonomy(taxonomy, search);
            IPagedList<T_Tag> MyList = MvcPaging.PagingExtensions.ToPagedList(Cate, currentPageIndex, GlobalVariables.PageSize , Cate.Count());
            return View(MyList);
        }

        [HttpGet]
        public ActionResult AddNew(string taxonomy)
        {
            ViewData["taxonomy"] = taxonomy;
            IEnumerable<T_Tag> TagList = _tagServices.GetByTaxonomy(taxonomy);
            TagViewModel model = new TagViewModel();
            model.TagList = TagList;
            return View("AddNew", model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNew(TagViewModel iTagVM)
        {
            ViewData["taxonomy"] = iTagVM.Taxonomy;
            if (ModelState.IsValid)
            {
                T_Tag NewTag = iTagVM.GetTag();
                _tagServices.AddNewTag(NewTag);
                return RedirectToAction("List", "Tag", new { @taxonomy = iTagVM.Taxonomy });
            }

            // Get Tag_List again
            iTagVM.TagList = _tagServices.GetByTaxonomy(iTagVM.Taxonomy);
            return View(iTagVM); 
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Tag Tag = _tagServices.GetByID((int)id);
            ViewData["taxonomy"] = Tag.Taxonomy;
            IEnumerable<T_Tag> TagList = _tagServices.GetByTaxonomy(Tag.Taxonomy);
            TagViewModel model = new TagViewModel(Tag, TagList);
            return View("Delete", model);
        }



        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            T_Tag Tag = _tagServices.GetByID((int)id);
            ViewData["taxonomy"] = Tag.Taxonomy;
            _tagServices.DeleteTag(id);
            return RedirectToAction("List", "Tag", new { @taxonomy = Tag.Taxonomy });
        }
    }
}
