using System;
using System.IO;
using HMS.Entities;
using HMS.Services;
using System.Web.Mvc;
using System.Collections.Generic;


namespace HMS.Areas.Dashboard.Controllers
{
    /// [Authorize(Roles = "Administrator")]
    public class DashboardController : Controller
    {
        // GET: Dashboard/Dashboard
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult UploadPictures()
        {
            JsonResult jResult = new JsonResult();
            var srvDash = new DashboardService();
            var oList = new List<Picture>();
            var oFiles = Request.Files;

            for (var i = 0; i < oFiles.Count; i++)
            {
                var oPic = oFiles[i];
                var oFile = Guid.NewGuid() + Path.GetExtension(oPic.FileName);
                var oPath = Server.MapPath("~/images/site/") + oFile;
                oPic.SaveAs(oPath);

                // For Database Part
                var oDbPic = new Picture { PicUrl = oFile };
                if (srvDash.SavePicture(oDbPic))
                {
                    oList.Add(oDbPic);
                }
            }
            jResult.Data = oList;
            return jResult;
        }
    }
}