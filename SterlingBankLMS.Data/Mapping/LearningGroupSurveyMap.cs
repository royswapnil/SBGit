using SterlingBankLMS.Data.Models.Entities;
using System.Data.Entity.ModelConfiguration;

namespace SterlingBankLMS.Data.Mapping
{
    public class LearningGroupSurveyMap : EntityTypeConfiguration<LearningGroupSurvey>
    {
        public LearningGroupSurveyMap()
        {
            HasRequired(x => x.Survey).WithMany().HasForeignKey(x => x.SurveyId).WillCascadeOnDelete(false);
            HasRequired(x => x.LearningGroup).WithMany(x => x.Surveys).HasForeignKey(x => x.LearningGroupId).WillCascadeOnDelete(false);
        }
    }
}