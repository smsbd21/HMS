using HMS.Models;
using System.Linq;
using HMS.Entities;
using HMS.Services;
using System.Web.Mvc;
using HMS.Areas.Dashboard.Models;
using System.Collections.Generic;

namespace HMS.Areas.Dashboard.Controllers
{
    public class AccomodationsController : Controller
    {
        DashboardService srvDash = new DashboardService();
        AccomodationService srvAcom = new AccomodationService();
        AccomodationPackageService srvPack = new AccomodationPackageService();
        // GET: Dashboard/Accomodations
        public ActionResult Index(string sAcom, int? iPack, int? iCurPage, int? iReco)
        {
            iCurPage = iCurPage ?? 1;
            iReco = iReco ?? srvAcom.iRecord;
            if (srvAcom.iRecord != iReco.Value) srvAcom.iRecord = iReco.Value;
            AccomodationsListingModel model = new AccomodationsListingModel
            {
                SrcAcom = sAcom,
                AccPackId = iPack,
                AccomodationPackages = srvPack.GetAllAccomodationPackage(),
                Accomodations = srvAcom.GetAccomodations(sAcom, iPack, srvAcom.iRecord, iCurPage.Value),
                Pager = new Pager(srvAcom.iTotalRec, iCurPage, srvAcom.iRecord)
            };
            return View(model);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            AccomodationActionModel model = new AccomodationActionModel
            {
                ID = srvAcom.GetAccomodationById(id).ID
            };
            return PartialView("_Delete", model);
        }
        [HttpGet]
        public ActionResult Action(int? id)
        {
            AccomodationActionModel model = new AccomodationActionModel();
            if (id.HasValue && id.Value > 0)
            {
                //data will editable
                var oAcom = srvAcom.GetAccomodationById(id.Value);
                model.ID = oAcom.ID;
                model.AccomodationPackageID = oAcom.AccomodationPackageId;
                model.AccName = oAcom.AccName;
                model.Description = oAcom.Description;
                model.AccomPictures = srvAcom.GetPicturesByAcomId(id.Value);
            }
            model.AccomodationPackages = srvPack.GetAllAccomodationPackage();

            return PartialView("_Action", model);
        }
        [HttpPost]
        public JsonResult Action(AccomodationActionModel model)
        {
            var bResult = false;
            JsonResult json = new JsonResult();
            // model.PictureIds = "90,67,23" => ["90", "67", "23"] => {90, 67, 23}
            List<int> iPics = string.IsNullOrEmpty(model.PictureIds) ? new List<int>() : model.PictureIds.Split(',').Select(x => int.Parse(x)).ToList();
            var oPics = srvDash.GetPictureByIds(iPics);

            if (model.ID > 0)
            {
                //data will be edit
                var oAcom = srvAcom.GetAccomodationById(model.ID);
                oAcom.AccomodationPackageId = model.AccomodationPackageID;
                oAcom.AccName = model.AccName;
                oAcom.Description = model.Description;
                oAcom.AccomPictures.Clear();
                oAcom.AccomPictures.AddRange(oPics.Select(x => new AccomPicture() { PictureId = x.ID,  AccomodationId = oAcom.ID }));
                bResult = srvAcom.SetAccomodation(oAcom, "Edit");
            }
            else
            {
                //data will be insert
                Accomodation oAcom = new Accomodation
                {
                    AccomodationPackageId = model.AccomodationPackageID,
                    AccName = model.AccName,
                    Description = model.Description,
                    AccomPictures = new List<AccomPicture>()
                };
                oAcom.AccomPictures.AddRange(oPics.Select(x => new AccomPicture() { PictureId = x.ID }));
                bResult = srvAcom.SetAccomodation(oAcom, "Save");
            }
            if (bResult) json.Data = new { Success = true };
            else json.Data = new { Success = false, Message = "Unable to perform action accomodation." };
            return json;

        }
        [HttpPost]
        public JsonResult Delete(AccomodationActionModel model)
        {
            JsonResult json = new JsonResult();
            var oAcom = srvAcom.GetAccomodationById(model.ID);
            var bResult = srvAcom.SetAccomodation(oAcom, "Delete");
            if (bResult) json.Data = new { Success = true };
            else json.Data = new { Success = false, Message = "Unable to delete accomodation." };
            return json;

        }
    }
}