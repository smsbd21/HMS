using HMS.Models;
using System.Web;
using System.Linq;
using HMS.Entities;
using HMS.Services;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using HMS.Areas.Dashboard.Models;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.Owin;

namespace HMS.Areas.Dashboard.Controllers
{
    public class UsersController : Controller
    {
        public int iRecord = 5;
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

        public UsersController()
        {
        }
        public UsersController(HMSRoleManager roleManager, HMSUserManager userManager, HMSSignInManager signInManager)
        {
            RoleManager = roleManager;
            UserManager = userManager;
            SignInManager = signInManager;
        }

        // GET: GetUsersInfo Method
        public async Task<IEnumerable<HMSUser>> GetUsersInfoAsync(string sUser, string sRole, int iReco, int iCurPage)
        {
            var oUser = UserManager.Users.AsQueryable();
            if (!string.IsNullOrEmpty(sRole))
            {
                var oRole = await RoleManager.FindByIdAsync(sRole);
                var oUserList = oRole.Users.Select(x => x.UserId).ToList();
                oUser = oUser.Where(x => oUserList.Contains(x.Id));
            }
            // Below code can obtain long time
            // if (!string.IsNullOrEmpty(sRole)) oUser = oUser.Where(x => x.Roles.Select(a => a.RoleId).Contains(sRole));
            if (!string.IsNullOrEmpty(sUser)) oUser = oUser.Where(x => x.FullName.ToLower().Contains(sUser.ToLower()));
            
            // Showing pagination
            iTotalRec = oUser.Count();
            //Working pagination with icons
            int iSkip = iReco * (iCurPage - 1);

            return oUser.OrderBy(x => x.FullName).Skip(iSkip).Take(iReco).ToList();
        }

        // GET: Dashboard/Users
        public async Task<ActionResult> Index(string sUser, string sRole, int? iCurPage, int? iReco)
        {
            iCurPage = iCurPage ?? 1;
            iReco = iReco ?? iRecord;
            if (iRecord != iReco.Value) iRecord = iReco.Value;
            UsersListingModel model = new UsersListingModel
            {
                RoleId = sRole,
                SrcUser = sUser,
                Roles = RoleManager.Roles.ToList(),
                Users = await GetUsersInfoAsync(sUser, sRole, iRecord, iCurPage.Value),
                Pager = new Pager(iTotalRec, iCurPage, iRecord)
            };
            return View(model);
        }
        [HttpGet]
        public async Task<ActionResult> Action(string id, bool isDel = false)
        {
            string sView = "_Action";            
            UserActionModel model = new UserActionModel();
            if (!string.IsNullOrEmpty(id))
            {
                //data will editable
                var oUser = await UserManager.FindByIdAsync(id);
                model.ID = oUser.Id;
                if (isDel) sView = "_Delete";
                else
                {
                    model.FullName = oUser.FullName;
                    model.PhoneNumber = oUser.PhoneNumber;
                    model.UserName = oUser.UserName;
                    model.Email = oUser.Email;
                    model.City = oUser.City;
                    model.Country = oUser.Country;
                    model.Address = oUser.Address;
                    model.PasswordHash = oUser.PasswordHash;
                }
            }
            return PartialView(sView, model);

            //if (!string.IsNullOrEmpty(id))
            //{
            //    //data will editable
            //    var oUser = await UserManager.FindByIdAsync(id);
            //    model.ID = oUser.Id;
            //    model.FullName = oUser.FullName;
            //    model.PhoneNumber = oUser.PhoneNumber;
            //    model.UserName = oUser.UserName;
            //    model.Email = oUser.Email;
            //    model.City = oUser.City;
            //    model.Country = oUser.Country;
            //    model.Address = oUser.Address;
            //}
            //return PartialView("_Action", model);
        }
        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            UserActionModel model = new UserActionModel();
            var oUser = await UserManager.FindByIdAsync(id);
            model.ID = oUser.Id;
            return PartialView("_Delete", model);
        }
        [HttpGet]
        public async Task<ActionResult> Privilege(string ID)
        {
            UserRolesModel model = new UserRolesModel();
            if (!string.IsNullOrEmpty(ID))
            {
                model.UserId = ID;
                var oUser = await UserManager.FindByIdAsync(ID);
                var oRoleList = oUser.Roles.Select(x => x.RoleId).ToList();
                model.Roles = RoleManager.Roles.Where(x => !oRoleList.Contains(x.Id)).ToList();
                model.UserRoles = RoleManager.Roles.Where(x => oRoleList.Contains(x.Id)).ToList();
            }
            return PartialView("_Privilege", model);
        }
        [HttpPost]
        public async Task<JsonResult> Action(UserActionModel model, bool isDel = false)
        {
            IdentityResult iResult = null;
            JsonResult json = new JsonResult();
            if (!string.IsNullOrEmpty(model.ID))
            {
                //data will be edit
                var oUser = await UserManager.FindByIdAsync(model.ID);
                if (isDel) iResult = await UserManager.DeleteAsync(oUser);
                else
                {
                    oUser.Id = model.ID;
                    oUser.FullName = model.FullName;
                    oUser.PhoneNumber = model.PhoneNumber;
                    oUser.UserName = model.UserName;
                    oUser.Email = model.Email;
                    oUser.City = model.City;
                    oUser.Country = model.Country;
                    oUser.Address = model.Address;
                    oUser.PasswordHash = model.PasswordHash;
                    iResult = await UserManager.UpdateAsync(oUser);
                }
            }
            else
            {
                //data will be insert
                HMSUser oUser = new HMSUser
                {
                    FullName = model.FullName,
                    PhoneNumber = model.PhoneNumber,
                    UserName = model.UserName,
                    Email = model.Email,
                    City = model.City,
                    Country = model.Country,
                    Address = model.Address,
                    PasswordHash=model.PasswordHash
                };
                iResult = await UserManager.CreateAsync(oUser);
            }
            if (iResult.Succeeded) json.Data = new { Success = true };
            else json.Data = new { Success = false, Message = string.Join(",", iResult.Errors) };
            //json.Data = new { Success = iResult.Succeeded, Message = string.Join(",", iResult.Errors) };
            return json;

        }
        [HttpPost]
        public async Task<JsonResult> Delete(UserActionModel model)
        {
            JsonResult json = new JsonResult();
            var oUser = await UserManager.FindByIdAsync(model.ID);
            var iResult = await UserManager.DeleteAsync(oUser);
            if (iResult.Succeeded) json.Data = new { Success = true };
            else json.Data = new { Success = false, Message = string.Join(",", iResult.Errors)};
            return json;

        }
        [HttpPost]
        public async Task<JsonResult> Privilege(string userId, string roleId, bool isDelete = false)
        {
            JsonResult json = new JsonResult();
            var oUser = await UserManager.FindByIdAsync(userId);
            var oRole = await RoleManager.FindByIdAsync(roleId);
            if (oUser != null && oRole != null)
            {
                IdentityResult iResult = null;
                if (!isDelete) iResult = await UserManager.AddToRoleAsync(userId, oRole.Name);
                else iResult = await UserManager.RemoveFromRoleAsync(userId, oRole.Name);

                json.Data = new { Success = iResult.Succeeded, Message = string.Join(",", iResult.Errors) };
            }
            else json.Data = new { Success = false, Message = "Invalid operation." };

            return json;
        }
    }
}