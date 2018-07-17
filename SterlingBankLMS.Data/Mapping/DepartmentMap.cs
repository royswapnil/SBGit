using SterlingBankLMS.Data.Models.Entities;
using System.Data.Entity.ModelConfiguration;

namespace SterlingBankLMS.Data.Mapping
{
    public class DepartmentMap : EntityTypeConfiguration<Department>
    {
        public DepartmentMap()
        {
            HasOptional(x => x.Group).WithMany().HasForeignKey(x=>x.GroupId).WillCascadeOnDelete(false);

        }
    }
}