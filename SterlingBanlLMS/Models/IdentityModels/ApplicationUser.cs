using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace SterlingBankLMS.Web.Models.IdentityModels
{
    public class ApplicationUser : IdentityUser<int, UserLogin, UserRole, UserClaim>, IUser<int>
    { 
        public string PictureUrl { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? LastLoginDateUtc { get; set; }

        public DateTime JoinDateUtc { get; set; }
    }
}