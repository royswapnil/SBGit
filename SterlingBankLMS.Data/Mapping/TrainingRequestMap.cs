using SterlingBankLMS.Data.Models;
using SterlingBankLMS.Data.Models.Entities;
using System.Data.Entity.ModelConfiguration;

namespace SterlingBankLMS.Data.Mapping
{
    public class TrainingRequestMap: EntityTypeConfiguration<TrainingRequest>
    {
        public TrainingRequestMap()
        {
            HasRequired(x => x.Training).WithMany().HasForeignKey(x => x.TrainingId).WillCascadeOnDelete(false);
            HasRequired(x => x.CreatedBy).WithMany().WillCascadeOnDelete(false);
        }
    }
}
