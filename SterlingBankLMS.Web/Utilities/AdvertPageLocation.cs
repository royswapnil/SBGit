using SterlingBankLMS.Web.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SterlingBankLMS.Web.Utilities
{
    public class AdvertPageLocation
    {
        public static readonly List<AdvertPages> AdLocations = new List<AdvertPages>(){
            new AdvertPages { ControllerName = "home", ActionName = "index", Location = AdLocation.Home },
            new AdvertPages { ControllerName = "Course", ActionName = "courses" , Location = AdLocation.CourseCatalogue },
            new AdvertPages { ControllerName = "Course", ActionName = "coursedetails",Location = AdLocation.CourseDetails },
            new AdvertPages { ControllerName = "Course", ActionName = "courselearningarea",Location = AdLocation.TakeCourse },
            new AdvertPages { ControllerName = "Examination", ActionName = "Index",Location = AdLocation.Exam },
            new AdvertPages { ControllerName = "Support", ActionName = "Index",Location = AdLocation.Support
            }
        };
    }

    public class AdvertPages
    {
        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public AdLocation Location { get; set; }

    }

}