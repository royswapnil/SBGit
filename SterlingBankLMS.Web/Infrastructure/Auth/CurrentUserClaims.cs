using Newtonsoft.Json;
using SterlingBankLMS.Web.Models.IdentityModels;
using SterlingBankLMS.Web.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace SterlingBankLMS.Web.Infrastructure.Auth
{
    public class UserClaims : ClaimsPrincipal
    {
        public UserClaims(IPrincipal principal) : base(principal)
        {
        }

        public int Id
        {
            get
            {
                if (FindFirst(ClaimTypes.NameIdentifier) == null)
                    return 0;

                return int.Parse(FindFirst(ClaimTypes.NameIdentifier).Value);
            }
        }

        public int UserId
        {
            get
            {
                var uid = FindFirst(AppConstants.ClaimsKey.UserId);
                if (uid == null)
                    return 0;

                int.TryParse(uid.Value, out int userId);
                return userId;
            }
        }

        public int OrganizationId
        {
            get
            {
                var organId = FindFirst(AppConstants.ClaimsKey.OrganizationId);
                if (organId == null)
                    return 0;

                int.TryParse(organId.Value, out int organizationId);
                return organizationId;
            }
        }

        public int? LineManagerId
        {
            get
            {
                var ManagerId = FindFirst(AppConstants.ClaimsKey.LineManagerId);

                if (ManagerId == null)
                    return null;

                if (int.TryParse(ManagerId.Value, out int lineManagerId))
                    return lineManagerId;
                return null;
            }
        }


        public string Email
        {
            get
            {
                var emailClaim = FindFirst(ClaimTypes.Email);
                if (emailClaim == null)
                    return string.Empty;

                return emailClaim.Value;
            }
        }

        public string UserName
        {
            get
            {
                var usernameClaim = FindFirst(ClaimTypes.Name);
                if (usernameClaim == null)
                    return "Anonymous";

                return usernameClaim.Value;
            }
        }

        public string FirstName
        {
            get
            {
                var usernameClaim = FindFirst(ClaimTypes.Surname);

                if (usernameClaim == null)
                    return string.Empty;

                return usernameClaim.Value;
            }
        }

        public string LastName
        {
            get
            {
                var usernameClaim = FindFirst(ClaimTypes.GivenName);

                if (usernameClaim == null)
                    return string.Empty;

                return usernameClaim.Value;
            }
        }

        public string BranchName
        {
            get
            {
                var branchnameClaim = FindFirst(AppConstants.ClaimsKey.Branch);
                if (branchnameClaim == null)
                    return "Unknown";
                return branchnameClaim.Value;
            }
        }
        public string StaffId
        {
            get
            {
                var staffidClaim = FindFirst(AppConstants.ClaimsKey.StaffId);
                if (staffidClaim == null)
                    return "0";
                return staffidClaim.Value;
            }
        }

        public string Function
        {
            get
            {
                var functionClaim = FindFirst(AppConstants.ClaimsKey.Function);
                if (functionClaim == null)
                    return "Unknown";
                return functionClaim.Value;
            }
        }

        public IEnumerable<string> Permissions
        {
            get
            {
                var permissionList = new List<string>();

                var permissionsClaim = FindFirst(AppConstants.ClaimsKey.Permissions);
                if (permissionsClaim != null) {
                    permissionList = JsonConvert.DeserializeObject<List<string>>(permissionsClaim.Value);
                }

                return permissionList;
            }
        }
    }

    public static class UserClaimsExtension
    {
        public static bool HasRole(this UserClaims currentUser, string role)
        {
            if (currentUser == null) {
                return false;
            }

            if (string.IsNullOrWhiteSpace(role)) {
                return false;
            }

            return currentUser.IsInRole(role);
        }

        public static bool HasPermission(this UserClaims user, Permission permission)
        {
            if (user == null || permission == null)
                return false;

            return user.Permissions.Contains(permission.SystemName);
        }
    }
}