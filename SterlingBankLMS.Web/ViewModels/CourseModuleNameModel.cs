using System.Collections.Generic;

namespace SterlingBankLMS.Web.ViewModels
{
    public class CourseModuleNameModel
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int SortOrder { get; set; }
        public IEnumerable<LessonNameModel> Lessons { get; set; }
    }
}