using SterlingBankLMS.Data.Models.Enums;

namespace SterlingBankLMS.Web.ViewModels
{
    public class CourseProgressModel
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public int CourseId { get; set; }
        public int Id { get; set; }
        public decimal Progress { get; set; }
        public string LearningArea { get; set; }
        public string CourseStatus { get; set; }

    }
}