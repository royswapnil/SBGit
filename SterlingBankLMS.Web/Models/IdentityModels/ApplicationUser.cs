using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Models.Enums;
using SterlingBankLMS.Web.Utilities;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SterlingBankLMS.Web.Models.IdentityModels
{
    public class ApplicationUser : IdentityUser<int, UserLogin, UserRole, UserClaim>, IUser<int>
    {
        public DateTime? Registration { get; set; }
        public string PictureUrl { get; set; }
        public bool SystemAccount { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserType UserType { get; set; }

        public DateTime? LastLoginDateUtc { get; set; }

        public int? OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }

        public virtual Branch Branch { get; set; }
        public int? BranchId { get; set; }

        public int? DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        public int? GroupId { get; set; }
        public virtual Group Group { get; set; }

        public int? RegionId { get; set; }
        public virtual Region Region { get; set; }

        public string StaffId { get; set; }
        public DateTime? DateOfEmployment { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Gender? Gender { get; set; }
        public int? GradeId { get; set; }
        public virtual Grade Grade { get; set; }

        public string LineManagerFirstName { get; set; }
        public string LineManagerLastName { get; set; }
        public string LineManagerStaffId { get; set; }

        public int? LineManagerId { get; set; }

        public string Functions { get; set; }
        public string Address { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, AppConstants.AuthenticationType);
            return userIdentity;
        }
    }

    public static class ApplicationUserExtension
    {
        public static bool IsAdminUser(this ApplicationUser user)
        {
            return user.SystemAccount == true;
        }

        public static bool HasExceededExpiryDate(this ApplicationUser user)
        {
            return user.LockoutEndDateUtc != null &&
              CommonHelper.GetCurrentDate() > user.LockoutEndDateUtc
                && !user.LockoutEnabled;
        }

        public static bool IsNull(this ApplicationUser user)
        {
            return user == null;
        }

        public static bool IsActiveDirectoryUser(this ApplicationUser user)
        {
            return user.UserType == UserType.ActiveDirectory;
        }

        public static bool IsNormalUser(this ApplicationUser user)
        {
            return user.UserType == UserType.Normal;
        }

        public static bool EmailNotConfirmed(this ApplicationUser user)
        {
            return user.EmailConfirmed == false;
        }

        public static bool AccountLocked(this ApplicationUser user)
        {
            return user.LockoutEnabled == true;
        }
    }
}