using System.Collections.Generic;

namespace SterlingBankLMS.Data.Models.Entities
{
    public class LearningGroup: OrganizationalBaseEntity
    {
        public string Name { get; set; }
        public string TagFormat { get; set; }
        public ICollection<LearningGroupTag> Tags { get; set; }
        public ICollection<LearningGroupCourse> Courses { get; set; }

        public ICollection<LearningGroupSurvey> Surveys { get; set; }

        public ICollection<LearningGroupExam> Exams { get; set; }

        public ICollection<LearningGroupTraining> Trainings { get; set; }

    }
}