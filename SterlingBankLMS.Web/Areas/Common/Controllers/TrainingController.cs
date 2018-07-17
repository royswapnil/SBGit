using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SterlingBankLMS.Web.Areas.Common.Controllers
{
public class TrainingController : Controller
    {
        // GET: Common/Training
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Calendar()
        {
            return View();
        }
    }
}