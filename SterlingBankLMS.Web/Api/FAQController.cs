using SterlingBankLMS.Core.Factories;
using SterlingBankLMS.Core.Helper;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Web.Infrastructure.Services;
using SterlingBankLMS.Web.Utilities;
using SterlingBankLMS.Web.Utilities.Extensions;
using SterlingBankLMS.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SterlingBankLMS.Web.Api
{
    [RoutePrefix("api/faq")]
    public class FAQController : BaseApiController
    {
        private readonly IWorkContext _workContext;
        public readonly FAQFactory _FAQFactory;
        private readonly IPermissionService _permissionSvc;

        public FAQController(IWorkContext workContext, FAQFactory FAQFactory,IPermissionService permissionSvc)
        {
            _workContext = workContext;
            _FAQFactory = FAQFactory;
            _permissionSvc = permissionSvc;
        }

        [HttpGet]
        [Route("GetFAQs")]
        public IHttpActionResult GetFAQs()
        {
            var result = new ApiResult<List<FAQModel>>();

            var faq = _FAQFactory.All(x => !x.IsDeleted, false).OrderBy(x=>x.SortOrder).ToList();

            var faqModel = faq.MapTo<List<FAQ>, List<FAQModel>>();
            result.HasError = false;
            result.Result = faqModel;
            return Ok(result);
        }

        [HttpPost]
        [Route("AddorUpdateFAQ")]
        public IHttpActionResult AddorUpdateFAQ(FAQModel model)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageFAQ))
            {
                return AccessDeniedResult();
            }
            var result = new ApiResult<FAQModel>();
           
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data provided");
            }

            var faq =  model.MapTo<FAQModel, FAQ>();

            faq = _FAQFactory.AddorUpdateFAQ(faq, _workContext.User.Id);
           
            var faqModel = faq.MapTo<FAQ, FAQModel>();
            result.HasError = false;
            result.Message = "Successfully created";
            result.Result = faqModel;
            return Ok(result);
        }

        [HttpPost]
        [Route("ResortFAQItems")]
        public IHttpActionResult ResortFAQItems(List<FAQModel> model)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageFAQ))
            {
                return AccessDeniedResult();
            }
            var result = new ApiResult<bool>();

            if (!ModelState.IsValid)    
                return BadRequest("Invalid data provided");

            var faqModelList = model.MapTo<List<FAQModel>, List<FAQ>>();
          
            var reSort = _FAQFactory.ResortFAQ(faqModelList, _workContext.User.Id);
            if (!reSort)
                return BadRequest("We encountered an error while processing your request. Please try again");

            result.HasError = false;
            return Ok(result);
        }

        [HttpDelete]
        [Route("Delete")]
        public IHttpActionResult Delete(int Id)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageFAQ))
            {
                return AccessDeniedResult();
            }

            //TODO: DELETE EXAM AS WELL

            var result = new ApiResult<CourseModel>();

          
            var faq = _FAQFactory.Find(Id);
            if (faq == null)
                return BadRequest("FAQ cannot be found");

            faq.IsDeleted = true;
            faq.LastModifiedById = _workContext.User.Id;
            faq.ModifiedDate = AppHelper.GetCurrentDate();
            _FAQFactory.Update(faq);
            _FAQFactory.RemoveCacheKey();

            result.HasError = false;
            result.Message = "FAQ successfully deleted";

            return Ok(result);
        }
    }
}
