using System.Web.Mvc;

namespace SterlingBankLMS.Web.Controllers.MvcControllers
{
    public class HomeController : BaseMvcController
    {
        //private readonly IBaseService<Course> _courseService;
        //public HomeController(IBaseService<Course> courseService)
        //{
        //    _courseService = courseService;
        //}
        public ActionResult Index()
        {
            return View();
        }
    }
}