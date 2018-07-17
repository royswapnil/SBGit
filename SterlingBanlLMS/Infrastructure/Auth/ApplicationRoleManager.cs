using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using SterlingBankLMS.Web.Models.IdentityModels;
using System.Data.Entity;

namespace SterlingBankLMS.Web.Infrastructure.Auth
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole, int>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole, int> store)
            : base(store)
        {
        }
        public static ApplicationRoleManager Create(
        IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            var roleManager = new ApplicationRoleManager(new ApplicationRoleStore(context.Get<DbContext>()));

            return roleManager;
        }
    }
}