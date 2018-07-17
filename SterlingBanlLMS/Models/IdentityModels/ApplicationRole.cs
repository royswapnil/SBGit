using Microsoft.AspNet.Identity.EntityFramework;

namespace SterlingBankLMS.Web.Models.IdentityModels
{
    public class ApplicationRole : IdentityRole<int, UserRole>
    {
        public bool IsActive { get; set; }
        public string SystemName { get; set; }
    }
}