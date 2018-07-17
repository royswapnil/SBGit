using System.Web.Mvc;

namespace SterlingBankLMS.Web.Areas.Common
{
    public class CommonAreaRegistration : AreaRegistration
    {
        public override string AreaName => "common";


        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
               "clav1",
               "course/learn/{id}/{title}",
              new
              {
                  area = "common",
                  controller = "course",
                  action = "courselearningarea",
              },
               new { id = @"\d+" });

            context.MapRoute(
               "clav2",
               "course/learn/{id}/{moduleId}/{title}",
              new
              {
                  area = "common",
                  controller = "course",
                  action = "courselearningarea",
              },
               new { id = @"\d+", moduleId = @"\d+" });

            context.MapRoute(
               "clav3",
               "course/learn/{id}/{moduleId}/{lessonId}/{title}",
              new
              {
                  area = "common",
                  controller = "course",
                  action = "courselearningarea",
              },
               new { id = @"\d+", moduleId = @"\d+", lessonId = @"\d+" });

            context.MapRoute(
              "clav4",
              "course/learn/lma/{id}/{moduleId}/{lessonId}/{title}",
             new
             {
                 area = "common",
                 controller = "course",
                 action = "courselearningarea",
             },
              new { id = @"\d+", moduleId = @"\d+", lessonId = @"\d+" });

            context.MapRoute(
                "coursedetails",
                "{controller}/{id}/{url}",
                new
                {
                    area = "common",
                    controller = "course",
                    action = "coursedetails"
                },
                new { id = @"\d+" }
            );

            context.MapRoute(
                "Common_default",
                "common/{controller}/{action}/{id}",
                new
                {
                    action = "index",
                    id = UrlParameter.Optional
                });
        }
    }
}