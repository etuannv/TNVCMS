using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using TNVCMS.Domain.Model;
using TNVCMS.Domain.Services;
using TNVCMS.Utilities;
using TNVCMS.Web.Areas.Admin.Models;

namespace TNVCMS.Web.Areas.Admin.Controllers
{
    public class SlideController : Controller
    {
        private readonly IT_SlideServices _SlideServices;


        public SlideController()
        {
            if (_SlideServices == null) _SlideServices = new T_SlideServices();
        }
        //
        // GET: /Admin/Slide/List
        [AcceptVerbs("GET")]
        public ActionResult List(string search, int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            ViewData["search"] = search;
            IEnumerable<T_Slide> Cate = _SlideServices.SlideSearch(search);
            IPagedList<T_Slide> MyList = MvcPaging.PagingExtensions.ToPagedList(Cate, currentPageIndex, GlobalVariables.PageSize, Cate.Count());
            return View(MyList);
        }


        // GET: /Admin/Slide/AddNew
        [AcceptVerbs("GET")]
        public ActionResult AddNew()
        {
            return View();
        }


        // POST: /Admin/Slide/AddNew
        [AcceptVerbs("POST")]
        [ValidateAntiForgeryToken]
        public ActionResult AddNew(T_Slide iSlide)
        {
            // Upload the image
            HttpPostedFileBase file = Request.Files["ImageData"];
            string PathReturn = UploadSlideImage(file);
            iSlide.ImagePath = PathReturn;
            ReturnValue<bool> result = new ReturnValue<bool>(false, "");
        
            if (ModelState.IsValid)
            {
                result = _SlideServices.AddNewSlide(iSlide);
            }
            if (result.RetValue)
            {
                return RedirectToAction("List", "Slide");
            }
            else
            {
                // Get Slide_List again
                ModelState.AddModelError("Error", result.Msg);
                return View(iSlide);
            }
        }

        private string UploadSlideImage(HttpPostedFileBase file)
        {
            if (!string.IsNullOrEmpty(file.FileName))
            {
                string RandomString = Path.GetRandomFileName();
                RandomString = RandomString.Replace(".", ""); // Remove period.

                String NewFileName = RandomString + file.FileName;
                var uploadDir = "/Content/Uploads/Slide";
                var ImageData = Path.Combine(Server.MapPath(uploadDir), NewFileName);
                var imageUrl = Path.Combine(uploadDir, NewFileName);
                file.SaveAs(ImageData);
                return imageUrl;
            }
            else
            {
                return "";
            }
        }


        // GET: /Admin/Slide/Delete
        [AcceptVerbs("GET")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Slide model = _SlideServices.GetByID((int)id);
            return View("Delete", model);
        }


        // POST: /Admin/Slide/Delete
        [ValidateAntiForgeryToken]
        [AcceptVerbs("POST")]
        public ActionResult Delete(int id)
        {
            T_Slide Slide = _SlideServices.GetByID((int)id);
            _SlideServices.DeleteSlide(id);
            //TODO: Update parent tree
            return RedirectToAction("List", "Slide");
        }

        // GET: /Admin/Slide/Edit
        [AcceptVerbs("GET")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Slide model = _SlideServices.GetByID((int)id);
            return View("Edit", model);
        }


        // POST: /Admin/Slide/Edit
        [ValidateAntiForgeryToken]
        [AcceptVerbs("POST")]
        public ActionResult Edit(T_Slide iSlide)
        {
            // Upload image if it have
            HttpPostedFileBase file = Request.Files["ImageData"];
            string PathReturn = UploadSlideImage(file);
            if (!string.IsNullOrEmpty(PathReturn)) iSlide.ImagePath = PathReturn;

            ReturnValue<bool> result = _SlideServices.UpdateSlide(iSlide);
            if (result.RetValue)
            {
                return RedirectToAction("List", "Slide");
            }
            else
            {
                // Get Slide_List again
                ModelState.AddModelError("Error", result.Msg);
                return View(iSlide);
            }
        }

        [AcceptVerbs("GET")]
        public JsonResult SlideSearch(string term)
        {
            return this.Json( _SlideServices.SlideSearch(term), JsonRequestBehavior.AllowGet);
        }

    }
}
