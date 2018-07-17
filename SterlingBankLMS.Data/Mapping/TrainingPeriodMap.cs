using SterlingBankLMS.Data.Models;
using SterlingBankLMS.Data.Models.Entities;
using System.Data.Entity.ModelConfiguration;

namespace SterlingBankLMS.Data.Mapping
{
    public class TrainingPeriodMap : EntityTypeConfiguration<TrainingPeriod>
    {
        public TrainingPeriodMap()
        {
            HasRequired(x => x.Training).WithMany(x=> x.TrainingPeriod).HasForeignKey(x => x.TrainingId).WillCascadeOnDelete(false);
            HasRequired(x => x.CreatedBy).WithMany().WillCascadeOnDelete(false);
            HasRequired(x => x.Organization).WithMany().HasForeignKey(x => x.OrganizationId).WillCascadeOnDelete(false);
        }
    }
}