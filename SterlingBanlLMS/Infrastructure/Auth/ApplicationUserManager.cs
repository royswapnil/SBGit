using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SterlingBankLMS.Web.Models.IdentityModels;
using System;

namespace SterlingBankLMS.Web.Infrastructure.Auth
{
    public class ApplicationUserManager : UserManager<ApplicationUser, int>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser, int> store, IdentityFactoryOptions<ApplicationUserManager> options)
           : base(store)
        {
            UserValidator = new UserValidator<ApplicationUser, int>(this)
            {
                AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = true,
            };

            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6
            };

            // Configure user lockout defaults
            UserLockoutEnabledByDefault = true;
            DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(10);
            MaxFailedAccessAttemptsBeforeLockout = 5;

            //var dataProtectionProvider = Startup.DataProtectionProvider; //options.DataProtectionProvider;
            //if (dataProtectionProvider != null) {
            //    UserTokenProvider =
            //        new DataProtectorTokenProvider<ApplicationUser, int>(dataProtectionProvider.Create("ASP.NET Identity"))
            //        {
            //            TokenLifespan = TimeSpan.FromHours(24),
            //        };
            //}
        }
    }
}