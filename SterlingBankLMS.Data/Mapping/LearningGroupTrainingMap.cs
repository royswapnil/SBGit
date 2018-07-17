using SterlingBankLMS.Data.Models;
using SterlingBankLMS.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Data.Mapping
{
    public class LearningGroupTrainingMap : EntityTypeConfiguration<LearningGroupTraining>
    {
        public LearningGroupTrainingMap() { 
            HasRequired(x => x.Training).WithMany().HasForeignKey(x => x.TrainingId).WillCascadeOnDelete(false);
            HasRequired(x => x.LearningGroup).WithMany(x => x.Trainings).HasForeignKey(x => x.LearningGroupId).WillCascadeOnDelete(false);
            HasRequired(x => x.CreatedBy).WithMany().WillCascadeOnDelete(false);
        }

    }
}
