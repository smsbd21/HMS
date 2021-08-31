using HMS.Models;
using HMS.Services;
using System.Web.Mvc;

namespace HMS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            AccomodationService srvAcom = new AccomodationService();
            AccomodationTypeService srvType = new AccomodationTypeService();
            AccomodationPackageService srvPack = new AccomodationPackageService();

            HomeViewModel model = new HomeViewModel
            {
                Accomodations = srvAcom.GetAllAccomodations(),
                AccomodationTypes = srvType.GetAccomodationTypes(null),
                AccomodationPackages = srvPack.GetAllAccomodationPackage()
            };
            return View(model);
        }
        public ActionResult Rooms()
        {
            return View();
        }
        public ActionResult Blog()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Dining()
        {
            return View();
        }
        public ActionResult Aminity()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
    }
}