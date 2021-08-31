using HMS.Entities;
using HMS.Services;
using System.Web.Mvc;
using HMS.Areas.Dashboard.Models;

namespace HMS.Areas.Dashboard.Controllers
{
    public class AccomodationTypesController : Controller
    {
        AccomodationTypeService srvType = new AccomodationTypeService();

        // GET: Dashboard/AccomodationTypes
        public ActionResult Index(string strType)
        {
            AccomodationTypesListingModel model = new AccomodationTypesListingModel
            {
                SrcType = strType,
                AccomodationTypes = srvType.GetAccomodationTypes(strType)
            };
            return View(model);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            AccomodationTypeActionModel model = new AccomodationTypeActionModel
            {
                ID = srvType.GetAccomodationTypeById(id).ID
            };
            return PartialView("_Delete", model);
        }
        [HttpGet]
        public ActionResult Action(int? id)
        {
            AccomodationTypeActionModel model = new AccomodationTypeActionModel();

            if (id.HasValue)
            {
                //data will editable
                var accType = srvType.GetAccomodationTypeById(id.Value);
                model.ID = accType.ID;
                model.AccType = accType.AccType;
                model.Description = accType.Description;
            }

            return PartialView("_Action", model);
        }
        [HttpPost]
        public JsonResult Action(AccomodationTypeActionModel model)
        {
            var bResult = false;
            JsonResult json = new JsonResult();
            if (model.ID > 0)
            {
                //data will be edit
                var accType = srvType.GetAccomodationTypeById(model.ID);
                accType.AccType = model.AccType;
                accType.Description = model.Description;
                //bResult = srvType.EditAccomodationType(accType);
                bResult = srvType.SetAccomodationType(accType,"Edit");
            }
            else
            {
                //data will be insert
                AccomodationType accType = new AccomodationType();
                accType.AccType = model.AccType;
                accType.Description = model.Description;
                //bResult = srvType.SaveAccomodationType(accType);
                bResult = srvType.SetAccomodationType(accType, "Save");
            }
            if (bResult) json.Data = new { Success = true };
            else json.Data = new { Success = false, Message = "Unable to perform action accomodation type." };
            return json;

        }
        [HttpPost]
        public JsonResult Delete(AccomodationTypeActionModel model)
        {
            JsonResult json = new JsonResult();
            var accType = srvType.GetAccomodationTypeById(model.ID);
            //var bResult = srvType.DeleteAccomodationType(accType);
            var bResult = srvType.SetAccomodationType(accType, "Delete");
            if (bResult) json.Data = new { Success = true };
            else json.Data = new { Success = false, Message = "Unable to delete accomodation type." };
            return json;

        }
    }
}