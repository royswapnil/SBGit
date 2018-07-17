using SterlingBankLMS.Data.Models;
using System.Data.Entity.ModelConfiguration;

namespace SterlingBankLMS.Data.Mapping
{
    class UserExaminationMap:EntityTypeConfiguration<UserExamination>
    {
        public UserExaminationMap()
        {
        }
    }
}