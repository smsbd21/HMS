using HMS.Models;
using System.Linq;
using HMS.Services;
using System.Web.Mvc;

namespace HMS.Controllers
{
    public class AccomodationsController : Controller
    {
        AccomodationTypeService srvTypes = new AccomodationTypeService();
        AccomodationPackageService srvPack = new AccomodationPackageService();
        AccomodationService srvAcom = new AccomodationService();
        PackageBookingService srvBook = new PackageBookingService();

        public ActionResult Index(int iType, int? iPack)
        {
            AccomodationsViewModel oModel = new AccomodationsViewModel
            {
                AccomodationType = srvTypes.GetAccomodationTypeById(iType),
                AccomodationPackages = srvPack.GetPackagesByTypeId(iType)
            };
            oModel.PackId = iPack ?? oModel.AccomodationPackages.First().ID;
            oModel.Accomodations = srvAcom.GetAccomodationsByPackId(oModel.PackId);

            return View(oModel);
        }
        public ActionResult Details(int iAcom)
        {
            PackageDetailsModel model = new PackageDetailsModel
            {
                Accomodation = srvAcom.GetAccomodationById(iAcom)
            };
            return View(model);
        }
        public ActionResult Booking(int iPack)
        {
            PackageBookingModel model = new PackageBookingModel
            {
                AccomodationPackage = srvPack.GetAccomodationPackageById(iPack)
            };

            return View(model);
        }
        public ActionResult Availability(AvailabilityModel model)
        {
            //if (model.PackId > 0)
            {
                var x = model.GuestName;
                //model.ContactNo;
                var e = model.Email;
                var p = model.NoOfAdults;
                var c = model.NoOfChilds;
                var f = model.FromDate;
                var d = model.Duration;
                //model.CheckOut;
                //model.Notes;

                //data will be check
                var oAcom = srvBook.GetAccomodations();

                var n = model.Notes;

                if (model.NoOfAdults > 9)
                {
                    var oPack = srvBook.GetBookings();
                }


                // data will be edit
                //var oPack = srvPack.GetAccomodationPackageById(model.PackId);
                //oPack.AccomodationTypeId = model.AccomodationTypeID;
                //oPack.PackageName = model.PackageName;
                //oPack.NoOfRoom = model.NoOfRoom;
                //oPack.FeePerNight = model.FeePerNight;

                //oPack.PackagePictures.Clear();
                //oPack.PackagePictures.AddRange(oPics.Select(x => new PackagePicture() { PictureId = x.ID, AccomodationPackageId = oPack.ID }));

                //bResult = srvPack.SetAccomodationPackage(oPack, "Edit");
            }

            return View();
        }
    }
}