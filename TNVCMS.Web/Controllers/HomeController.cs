using System.Web.Mvc;
using TNVCMS.Domain.Services;
using TNVCMS.Web.Models;

namespace TNVCMS.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IT_NewsServices _newsServices;

        public HomeController(IT_NewsServices newsServices)
        {
            _newsServices = newsServices;
        }

        public ActionResult Index()
        {
            return View(new HomeViewModel()
                {
                    T_Newses = _newsServices.GetAllNews()
                });
        }

    }
}
