using SterlingBankLMS.Data.Models.Entities;
using System.Data.Entity.ModelConfiguration;

namespace SterlingBankLMS.Data.Mapping
{
    public class UserCourseMap : EntityTypeConfiguration<UserCourse>
    {
        public UserCourseMap()
        {
            HasRequired(x => x.User).WithMany().HasForeignKey(x => x.UserId).WillCascadeOnDelete(false);

            HasRequired(x => x.CreatedBy).WithMany().WillCascadeOnDelete(false);
        }
    }
}