using SterlingBankLMS.Data.Models.Entities;
using System.Data.Entity.ModelConfiguration;

namespace SterlingBankLMS.Data.Mapping
{
    public class LearningGroupMap : EntityTypeConfiguration<LearningGroup>
    {
        public LearningGroupMap()
        {
            Property(x => x.TagFormat).IsUnicode(false);
        }
    }
}