using SterlingBankLMS.Core.DTO;

namespace SterlingBankLMS.Web.ViewModels
{
    public class CourseDetailModel
    {
        public AdvertDto TopSectionAd { get; set; }
        public string Slug { get; set; }
        public string Objectives { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public int ExamRetakeCount { get; set; }
        public string DueDate { get; set; }
        public string LearningArea { get; set; }
        public string Overview { get; set; }
        public int? EstimatedDuration { get; set; }
        public int? HoursPerWeek { get; set; }
        public int? ModulesCount { get; set; }
    }
}