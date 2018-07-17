using SterlingBankLMS.Web.Controllers;
using SterlingBankLMS.Web.Infrastructure.Services;
using SterlingBankLMS.Web.Models.IdentityModels;
using SterlingBankLMS.Web.Utilities.Extensions;
using SterlingBankLMS.Web.ViewModels;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SterlingBankLMS.Web.Areas.Common.Controllers
{
    public class ProfileController : BaseController
    {
        // GET: Common/Profile
        private readonly IUserAccountService _accountService;
        private readonly IWorkContext _workContext;
        public ProfileController(IUserAccountService accountService, IWorkContext workContext)
        {
            _accountService = accountService;
            _workContext = workContext;
        }


        public async Task<ActionResult> Index()
        {
            var acct = await _accountService.FindUserByIdAsync(_workContext.User.Id);

            if (acct == null)
                return NotFoundView();

            var model = acct.MapTo<ApplicationUser, EmployeeProfileModel>();

            return View(model);
        }
    }
}