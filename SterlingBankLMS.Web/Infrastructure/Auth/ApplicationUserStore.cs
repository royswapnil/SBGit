using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SterlingBankLMS.Web.Models.IdentityModels;
using System;
using System.Data.Entity;

namespace SterlingBankLMS.Web.Infrastructure.Auth
{
    /// <summary>
    /// Persistence Store for ApplicationUser
    /// </summary>
    public class ApplicationUserStore : UserStore<ApplicationUser, ApplicationRole, int, UserLogin, UserRole, UserClaim>,
        IUserStore<ApplicationUser, int>, IDisposable
    {
        public ApplicationUserStore(DbContext ctxt)
            : base(ctxt)
        {

        }
    }

    /// <summary>
    /// Persistence Store for ApplicationStore
    /// </summary>
    public class ApplicationRoleStore : RoleStore<ApplicationRole, int, UserRole>,
         IQueryableRoleStore<ApplicationRole, int>, IRoleStore<ApplicationRole, int>, IDisposable
    {
        public ApplicationRoleStore(DbContext ctxt)
            : base(ctxt)
        {

        }
    }
}