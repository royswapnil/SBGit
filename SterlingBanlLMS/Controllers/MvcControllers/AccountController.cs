using System.Web.Mvc;

namespace SterlingBankLMS.Web.Controllers.MvcControllers
{
    public class AccountController : BaseMvcController
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
    }
}