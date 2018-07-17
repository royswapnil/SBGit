using SterlingBankLMS.Core.Factories;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Web.Controllers;
using SterlingBankLMS.Web.Utilities.Enums;
using SterlingBankLMS.Web.Utilities.Extensions;
using SterlingBankLMS.Web.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SterlingBankLMS.Web.Areas.Common.Controllers
{
    public class SupportController : BaseController
    {
        private readonly FAQFactory _fAQFactory;
        private readonly AdvertFactory _advertFactory;

        public SupportController(FAQFactory fAQFactory,AdvertFactory advertFactory)
        {
            _fAQFactory = fAQFactory;
            _advertFactory = advertFactory;

        }
        // GET: Common/Support
        public ActionResult Index()
        {
        var faqList = _fAQFactory.GetAllFAQ();
            var advert = _advertFactory.GetCurrentTopLocationBanner((int)AdLocation.Support);
            var model = new SupportFAQPageModel { FaqList = faqList.MapTo<List<FAQ>, List<FAQModel>>(),
                TopSectionAd = advert
            };
            return View(model);
        }
    }
}