using HMS.Entities;
using Microsoft.Owin;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;

namespace HMS.Services
{
    /// <summary>
    /// Customized the application sign-in manager which is used in this application.
    /// </summary>
    public class HMSSignInManager : SignInManager<HMSUser, string>
    {
        public HMSSignInManager(HMSUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(HMSUser user)
        {
            return user.GenerateUserIdentityAsync((HMSUserManager)UserManager);
        }

        public static HMSSignInManager Create(IdentityFactoryOptions<HMSSignInManager> options, IOwinContext context)
        {
            return new HMSSignInManager(context.GetUserManager<HMSUserManager>(), context.Authentication);
        }
    }
}
