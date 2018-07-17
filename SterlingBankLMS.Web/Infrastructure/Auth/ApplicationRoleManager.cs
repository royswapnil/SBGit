using Microsoft.AspNet.Identity;
using SterlingBankLMS.Web.Models.IdentityModels;

namespace SterlingBankLMS.Web.Infrastructure.Auth
{
    /// <summary>
    /// Application Role Manager
    /// </summary>
    public class ApplicationRoleManager : RoleManager<ApplicationRole, int>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole, int> store)
            : base(store)
        {
        }
    }
}