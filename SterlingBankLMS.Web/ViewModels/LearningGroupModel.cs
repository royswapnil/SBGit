using SterlingBankLMS.Data.Models.Enums;
using System.Collections.Generic;

namespace SterlingBankLMS.Web.ViewModels
{



    public class LearningGroupModel
    {

        public class Group : BaseModel
        {
            public string Name { get; set; }
            public List<Tags> Tags { get; set; }

            public List<Courses> Courses { get; set; }
            public string TagFormat { get; set; }
        }

        public class Tags
        {

            public int LearningGroupId { get; set; }
            public string Name { get; set; }
            public LearningGroupTagType TagType { get; set; }
            public int TagValue { get; set; }

        }

        public class Courses : BaseModel
        {
            public int CourseId { get; set; }
            public int LearningGroupId { get; set; }
            public UserCourseAvailability Availability { get; set; }
            public bool Required { get; set; }
            public bool Assigned { get; set; }
        }

        public class Exams : BaseModel
        {
            public int ExaminationId { get; set; }
            public int LearningGroupId { get; set; }
        }
        public class Surveys : BaseModel
        {
            public int SurveyId { get; set; }
            public int LearningGroupId { get; set; }
        }

        public class Trainings : BaseModel
        {
            public int TrainingId { get; set; }
            public int LearningGroupId { get; set; }
        }

    }
}