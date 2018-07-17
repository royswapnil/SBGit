using SterlingBankLMS.Core.DTO;
using System.Collections.Generic;

namespace SterlingBankLMS.Web.ViewModels
{
    public class EmployeeHomePageModel
    {
        public AdvertDto TopSectionAd { get; set; }

        public IEnumerable<AssignedCourseModel> AssignedCourses { get; set; }
        public IEnumerable<AssignedCourseModel> RecommendedCourses { get; set; }

        public bool AssignedCoursesHasMore { get; set; }

        public bool RecommendedCoursesHasMore { get; set; }
    }
}