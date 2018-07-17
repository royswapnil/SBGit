using System.ComponentModel;

namespace SterlingBankLMS.Data.Models.Enums
{
    public enum SurveyType
    {
        [Description("Course Survey")]
        CourseSurvey = 1,
        [Description("Exam Survey")]
        ExamSurvey,
        [Description("Training Survey")]
        TrainingSurvey,
        [Description("Independent Survey")]
        IndependentSurvey
    }
}
