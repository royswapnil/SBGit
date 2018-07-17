using SterlingBankLMS.Data.Models.Entities;
using System.Data.Entity.ModelConfiguration;

namespace SterlingBankLMS.Data.Mapping
{
    public class CertificateMap : EntityTypeConfiguration<Certificate>
    {
        public CertificateMap()
        {
            HasRequired(x => x.Course).WithMany().HasForeignKey(x => x.CourseId).WillCascadeOnDelete(false);
            HasRequired(x => x.CreatedBy).WithMany().WillCascadeOnDelete(false);
            HasRequired(x => x.Organization).WithMany().HasForeignKey(x => x.OrganizationId).WillCascadeOnDelete(false);


        }
    }
}