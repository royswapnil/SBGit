using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Core.Factories;
using SterlingBankLMS.Web.Infrastructure.Services;
using SterlingBankLMS.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SterlingBankLMS.Web.Api
{
    [RoutePrefix("api/certificate")]
    public class CertificateController : BaseApiController
    {

        private readonly IWorkContext _workContext;
        public readonly UserCourseFactory _userCourseFactory;

        public CertificateController(IWorkContext workContext, UserCourseFactory userCourseFactory)
        {
            _workContext = workContext;
            _userCourseFactory = userCourseFactory;
        }
        

        [HttpGet]
        [Route("getPagedUserCertificates")]
        public IHttpActionResult getPagedUserCertificates(int pageSize, int pageNumber)
        {
            var result = new SearchResponse<CertificateDto>();
            var users = _userCourseFactory.GetPagedUserCertificate(_workContext.User.Id,pageSize,pageNumber,out int totalRecords);
            result.data = users;
            result.recordsTotal = totalRecords;
            return Ok(result);
        }


    }
}
