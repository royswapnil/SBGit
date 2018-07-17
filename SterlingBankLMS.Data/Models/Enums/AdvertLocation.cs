using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Data.Models.Enums
{
    public enum AdvertLocation
    {
        [Description("All Pages")]
        All = 1,
        [Description("Home Page")]
        Home ,
        [Description("Course Catalogue")]
        CourseCatalogue ,
        [Description("Course Details")]
        CourseDetails,
        [Description("Take Course")]
        TakeCourse,
        [Description("Examination Page")]
        Exam,
        [Description("Support")]
        Support 
    }
}
