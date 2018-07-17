using SterlingBankLMS.Web.Utility;
using System.Security.Claims;
using System.Security.Principal;

namespace SterlingBankLMS.Web.Infrastructure.Auth
{
    public class CurrentUserClaims : ClaimsPrincipal
    {
        public CurrentUserClaims(IPrincipal principal) : base(principal)
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

        public int BaseId
        {
            get
            {
                var baseId = FindFirst(AppConstants.Keys.UserId);
                if (baseId == null)
                    return 0;

                return int.Parse(baseId.Value);
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

        //public string PictureUrl
        //{
        //    get
        //    {
        //        var picClaim = FindFirst(AppKeys.ProfilePictureUrl);
        //        return picClaim?.Value;
        //    }
        //}

        //public IEnumerable<string> Permissions
        //{
        //    get
        //    {
        //        var permissionList = new List<string>();

        //        var permissionsClaim = FindFirst(AppKeys.Permissions);
        //        if (permissionsClaim != null) {
        //            permissionList = JsonConvert.DeserializeObject<List<string>>(permissionsClaim.Value);
        //        }
        //        return permissionList;
        //    }
        //}
    }
}