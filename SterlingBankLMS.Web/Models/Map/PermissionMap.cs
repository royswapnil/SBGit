using SterlingBankLMS.Web.Models.IdentityModels;
using System.Data.Entity.ModelConfiguration;

namespace SterlingBankLMS.Web.Models.Map
{
    public class PermissionMap : EntityTypeConfiguration<Permission>
    {
        public PermissionMap()
        {
            HasMany(p => p.Roles).WithMany(p => p.Permissions)
                .Map(x => x.ToTable("AspNetRolesPermission"));
        }
    }
}