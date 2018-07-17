using SterlingBankLMS.Data.Models.Entities;
using System.Data.Entity.ModelConfiguration;

namespace SterlingBankLMS.Data.Mapping
{
    public class ForumTopicsMap : EntityTypeConfiguration<ForumTopics>
    {
        public ForumTopicsMap()
        {
            HasRequired(x => x.CreatedBy).WithMany().WillCascadeOnDelete(false);
            HasRequired(x => x.Organization).WithMany().HasForeignKey(x => x.OrganizationId).WillCascadeOnDelete(false);
        }
    }
}