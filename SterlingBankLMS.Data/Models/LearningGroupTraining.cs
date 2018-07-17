using SterlingBankLMS.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Data.Models.Entities
{
    public class LearningGroupTraining : TrackableEntity
    {
        public int TrainingId { get; set; }
        public Training Training { get; set; }
        public int LearningGroupId { get; set; }
        public LearningGroup LearningGroup { get; set; }
    }
}
