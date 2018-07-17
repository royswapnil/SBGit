using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using SterlingBankLMS.Core.Factories;
using SterlingBankLMS.Web.Infrastructure.Services;
using SterlingBankLMS.Web.Models.IdentityModels;
using SterlingBankLMS.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SterlingBankLMS.Web.Infrastructure.Auth
{
    public class ApplicationUserManager : UserManager<ApplicationUser, int>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser, int> store)
           : base(store)
        {
            UserValidator = new UserValidator<ApplicationUser, int>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true,
            };
            // Configure validation logic for passwords
            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6
            };
            // Configure user lockout defaults
            UserLockoutEnabledByDefault = false;
            DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(60);
            MaxFailedAccessAttemptsBeforeLockout = 5;

            var dataProtectionProvider = Startup.DataProtectionProvider;
            if (dataProtectionProvider != null) {
                UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser, int>(dataProtectionProvider.Create("ASP.NET Identity"))
                    {
                        TokenLifespan = TimeSpan.FromDays(1),
                    };
            }

            ClaimsIdentityFactory = new ApplicationClaimsIdentity();
        }
    }

    public class ApplicationClaimsIdentity : ClaimsIdentityFactory<ApplicationUser, int>
    {
        public override async Task<ClaimsIdentity> CreateAsync(UserManager<ApplicationUser, int> manager, ApplicationUser appuser, string authenticationType)
        {
            var identity = await base.CreateAsync(manager, appuser, authenticationType);

            var userFactory = DependencyResolver.Current.GetService<UserFactory>();

            var user = userFactory.Find(p => p.ApplicationUserId == appuser.Id, false);
            var branch = appuser.Branch?.Name ?? "";

            var userPermissions = GetUserPermissions(appuser);
            var jUserPermissions = JsonConvert.SerializeObject(userPermissions);

            identity.AddClaim(new Claim(ClaimTypes.Email, appuser.Email ?? string.Empty));
            identity.AddClaim(new Claim(ClaimTypes.GivenName, appuser.FirstName ?? string.Empty));
            identity.AddClaim(new Claim(ClaimTypes.Surname, appuser.LastName ?? string.Empty));


            identity.AddClaim(new Claim(AppConstants.ClaimsKey.OrganizationId, appuser.OrganizationId?.ToString() ?? string.Empty));
            identity.AddClaim(new Claim(AppConstants.ClaimsKey.Function, appuser.Functions ?? string.Empty));
            identity.AddClaim(new Claim(AppConstants.ClaimsKey.StaffId, appuser.StaffId ?? "0"));
            identity.AddClaim(new Claim(AppConstants.ClaimsKey.Branch, branch));
            identity.AddClaim(new Claim(AppConstants.ClaimsKey.UserId, user?.Id.ToString() ?? string.Empty));
            identity.AddClaim(new Claim(AppConstants.ClaimsKey.Permissions, jUserPermissions ?? string.Empty));

            return identity;
        }

        private IEnumerable<string> GetUserPermissions(ApplicationUser user)
        {
            var userRolesIds = user.Roles.Select(p => p.RoleId);

            var roleSvc = DependencyResolver.Current.GetService<IUserRoleService>();

            var userRoles = roleSvc.GetRoles(p => p.IsActive && userRolesIds.Contains(p.Id));
            var userPermissions = userRoles.SelectMany(x => x.Permissions.Select(y => y.SystemName));

            return userPermissions;
        }
    }
}