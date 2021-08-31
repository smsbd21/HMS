using HMS.Models;
using System.Linq;
using HMS.Entities;
using HMS.Services;
using System.Web.Mvc;
using System.Collections.Generic;
using HMS.Areas.Dashboard.Models;

namespace HMS.Areas.Dashboard.Controllers
{
    public class AccomodationPackagesController : Controller
    {
        DashboardService srvDash = new DashboardService();
        AccomodationTypeService srvType = new AccomodationTypeService();
        AccomodationPackageService srvPack = new AccomodationPackageService();

        // GET: Dashboard/AccomodationPackages
        public ActionResult Index(string sPack, int? iType, int? iCurPage,int? iReco)
        {
            iCurPage = iCurPage ?? 1;
            iReco = iReco ?? srvPack.iRecord;
            if (srvPack.iRecord != iReco.Value) srvPack.iRecord = iReco.Value;      
            AccomodationPackagesListingModel model = new AccomodationPackagesListingModel
            {
                SrcPack = sPack,
                AccTypeId = iType,
                AccomodationTypes = srvType.GetAccomodationTypes(null),          
                AccomodationPackages = srvPack.GetAccomodationPackages(sPack, iType, srvPack.iRecord, iCurPage.Value),
                Pager = new Pager(srvPack.iTotalRec, iCurPage, srvPack.iRecord)
            };
            return View(model);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            AccomodationPackageActionModel model = new AccomodationPackageActionModel
            {
                ID = srvPack.GetAccomodationPackageById(id).ID
            };
            return PartialView("_Delete", model);
        }
        [HttpGet]
        public ActionResult Action(int? id)
        {
            AccomodationPackageActionModel model = new AccomodationPackageActionModel();
            if (id.HasValue && id.Value > 0)
            {
                // data will editable
                var oPack = srvPack.GetAccomodationPackageById(id.Value);
                model.ID = oPack.ID;
                model.AccomodationTypeID = oPack.AccomodationTypeId;
                model.PackageName = oPack.PackageName;
                model.NoOfRoom = oPack.NoOfRoom;
                model.FeePerNight = oPack.FeePerNight;
                model.PackagePictures = srvPack.GetPicturesByPackId(id.Value);
            }
            model.AccomodationTypes = srvType.GetAccomodationTypes(null);

            return PartialView("_Action", model);
        }
        [HttpPost]
        public JsonResult Action(AccomodationPackageActionModel model)
        {
            var bResult = false;
            JsonResult json = new JsonResult();
            // model.PictureIds = "90,67,23" => ["90", "67", "23"] => {90, 67, 23}
            List<int> iPics = string.IsNullOrEmpty(model.PictureIds) ? new List<int>() : model.PictureIds.Split(',').Select(x => int.Parse(x)).ToList();
            var oPics = srvDash.GetPictureByIds(iPics);

            if (model.ID > 0)
            {
                // data will be edit
                var oPack = srvPack.GetAccomodationPackageById(model.ID);
                oPack.AccomodationTypeId = model.AccomodationTypeID;
                oPack.PackageName = model.PackageName;
                oPack.NoOfRoom= model.NoOfRoom;
                oPack.FeePerNight = model.FeePerNight;

                oPack.PackagePictures.Clear();
                oPack.PackagePictures.AddRange(oPics.Select(x => new PackagePicture() { PictureId = x.ID, AccomodationPackageId = oPack.ID }));

                bResult = srvPack.SetAccomodationPackage(oPack, "Edit");
            }
            else
            {
                //data will be insert
                AccomodationPackage oPack = new AccomodationPackage
                {
                    AccomodationTypeId = model.AccomodationTypeID,
                    PackageName = model.PackageName,
                    NoOfRoom = model.NoOfRoom,
                    FeePerNight = model.FeePerNight,
                    PackagePictures = new List<PackagePicture>()
                };
                oPack.PackagePictures.AddRange(oPics.Select(x => new PackagePicture() { PictureId = x.ID }));

                bResult = srvPack.SetAccomodationPackage(oPack, "Save");
            }
            if (bResult) json.Data = new { Success = true };
            else json.Data = new { Success = false, Message = "Unable to perform action accomodation type." };
            return json;

        }
        [HttpPost]
        public JsonResult Delete(AccomodationPackageActionModel model)
        {
            JsonResult json = new JsonResult();
            var oPack = srvPack.GetAccomodationPackageById(model.ID);
            var bResult = srvPack.SetAccomodationPackage(oPack, "Delete");
            if (bResult) json.Data = new { Success = true };
            else json.Data = new { Success = false, Message = "Unable to delete accomodation type." };
            return json;

        }
    }
}