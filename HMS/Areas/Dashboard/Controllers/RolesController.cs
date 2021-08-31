using HMS.Models;
using System.Web;
using System.Linq;
using HMS.Services;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using HMS.Areas.Dashboard.Models;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HMS.Areas.Dashboard.Controllers
{
    public class RolesController : Controller
    {
        public int iTotalRec = 0;
        private HMSRoleManager _roleManager;
        private HMSUserManager _userManager;
        private HMSSignInManager _signInManager;
        public HMSRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<HMSRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        public HMSUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<HMSUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public HMSSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<HMSSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public RolesController()
        {
        }
        public RolesController(HMSRoleManager roleManager, HMSUserManager userManager, HMSSignInManager signInManager)
        {
            RoleManager = roleManager;
            UserManager = userManager;
            SignInManager = signInManager;
        }

        // GET: GetRolesInfo Method
        public IEnumerable<IdentityRole> GetRolesInfo(string sRole, int iReco, int iCurPage)
        {
            var oRole = RoleManager.Roles.AsQueryable();
            if (!string.IsNullOrEmpty(sRole))
                oRole = oRole.Where(x => x.Name.ToLower().Contains(sRole.ToLower()));
            
            // Showing pagination
            iTotalRec = oRole.Count();
            //Working pagination with icons
            int iSkip = iReco * (iCurPage - 1);

            return oRole.OrderBy(x => x.Name).Skip(iSkip).Take(iReco).ToList();
        }

        // GET: Dashboard/Roles
        public ActionResult Index(string sRole, int? iCurPage)
        {
            int iReco = 10;
            iCurPage = iCurPage ?? 1;

            RolesListingModel model = new RolesListingModel
            {
                SrcRole = sRole,
                Roles = GetRolesInfo(sRole, iReco, iCurPage.Value),
                Pager = new Pager(iTotalRec, iCurPage, iReco)
            };
            return View(model);
        }
        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            RoleActionModel model = new RoleActionModel();
            var oRole = await RoleManager.FindByIdAsync(id);
            model.ID = oRole.Id;
            return PartialView("_Delete", model);
        }
        [HttpGet]
        public async Task<ActionResult> Action(string id)
        {
            RoleActionModel model = new RoleActionModel();
            if (!string.IsNullOrEmpty(id))
            {
                //data will editable
                var oRole = await RoleManager.FindByIdAsync(id);
                model.ID = oRole.Id;
                model.Name = oRole.Name;
            }
            return PartialView("_Action", model);
        }

        [HttpPost]
        public async Task<JsonResult> Action(RoleActionModel model)
        {
            IdentityResult iResult = null;
            JsonResult json = new JsonResult();
            if (!string.IsNullOrEmpty(model.ID))
            {
                //data will be edit
                var oRole = await RoleManager.FindByIdAsync(model.ID);
                oRole.Id = model.ID;
                oRole.Name = model.Name;
                iResult = await RoleManager.UpdateAsync(oRole);
            }
            else
            {
                //data will be insert
                IdentityRole oRole = new IdentityRole
                {
                    Name = model.Name
                };
                iResult = await RoleManager.CreateAsync(oRole);
            }
            if (iResult.Succeeded) json.Data = new { Success = true };
            else json.Data = new { Success = false, Message = string.Join(",", iResult.Errors) };
            return json;

        }
        [HttpPost]
        public async Task<JsonResult> Delete(RoleActionModel model)
        {
            JsonResult json = new JsonResult();
            var oRole = await RoleManager.FindByIdAsync(model.ID);
            var iResult = await RoleManager.DeleteAsync(oRole);
            if (iResult.Succeeded) json.Data = new { Success = true };
            else json.Data = new { Success = false, Message = string.Join(",", iResult.Errors) };
            return json;

        }
    }
}