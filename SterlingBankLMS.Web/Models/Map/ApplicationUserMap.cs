using SterlingBankLMS.Web.Models.IdentityModels;
using System.Data.Entity.ModelConfiguration;

namespace SterlingBankLMS.Web.Models.Map
{
    public class ApplicationUserMap:EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserMap()
        {
            ToTable("ApplicationUser");
        }
    }
}