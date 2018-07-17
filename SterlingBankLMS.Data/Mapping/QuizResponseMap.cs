using SterlingBankLMS.Data.Models.Entities;
using System.Data.Entity.ModelConfiguration;

namespace SterlingBankLMS.Data.Mapping
{
    public class QuizResponseMap : EntityTypeConfiguration<QuizResponse>
    {
        public QuizResponseMap()
        {
            HasRequired(x => x.Quiz).WithMany().HasForeignKey(x => x.QuizId).WillCascadeOnDelete(false);
        }
    }
}