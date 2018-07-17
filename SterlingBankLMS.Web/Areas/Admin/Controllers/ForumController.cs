using SterlingBankLMS.Core.Factories;
using System.Web.Mvc;

namespace SterlingBankLMS.Web.Areas.Admin.Controllers
{
    public class ForumController : Controller
    {
        private readonly ForumFactory _forumFactory;
       
        public ForumController(ForumFactory forumFactory)
        {
            _forumFactory = forumFactory;          
        }

        // GET: Admin/Forum
        public ActionResult Index()
        {
            var successMsg = TempData["successMsg"];
            if (successMsg != null)
                ViewBag.SuccessMsg = successMsg;

            var errorMsg = TempData["errorMsg"];
            if (errorMsg != null)
                ViewBag.ErrorMsg = errorMsg;

            var categories = _forumFactory.GetAllCategories();
            ViewBag.Categories = categories;
            return View();
        }

        public ActionResult Add_category(FormCollection category)
        {
            string category_name = category["cat_name"].ToString();
            string desc = category["desc"].ToString();
            bool addCat = _forumFactory.AddCategory(category_name, desc);
            return RedirectToAction("Index");
        }
    }
}