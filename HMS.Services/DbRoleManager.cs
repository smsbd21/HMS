using HMS.Data;
using Microsoft.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HMS.Services
{
    public class HMSRoleManager : RoleManager<IdentityRole>
    {
        public HMSRoleManager(IRoleStore<IdentityRole, string> roleStore) : base(roleStore)
        {
        }
        public static HMSRoleManager Create(IdentityFactoryOptions<HMSRoleManager> options, IOwinContext context)
        {
            return new HMSRoleManager(new RoleStore<IdentityRole>(context.Get<HMSContext>()));
        }
    }
}
