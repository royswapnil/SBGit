using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Core.Factories;
using SterlingBankLMS.Web.Infrastructure.Services;
using SterlingBankLMS.Web.Utilities;
using SterlingBankLMS.Web.Utilities.Extensions;
using SterlingBankLMS.Web.ViewModels;
using System.Collections.Generic;
using System.Web.Http;

namespace SterlingBankLMS.Web.Api
{
    [RoutePrefix("Api/UserExamination")]
    public class UserExaminationController : BaseApiController
    {
        private readonly UserExaminationFactory _userExaminationFactory;
        private readonly IWorkContext _workContext;
        private readonly IPermissionService _permissionService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userExaminationFactory"></param>
        /// <param name="workContext"></param>
        /// <param name="permissionService"></param>
        public UserExaminationController(UserExaminationFactory userExaminationFactory, IWorkContext workContext, IPermissionService permissionService)
        {
            _userExaminationFactory = userExaminationFactory;
            _workContext = workContext;
            _permissionService = permissionService;
        }

        [HttpGet]
        [Route("GetEmployeeExams")]
        public IHttpActionResult GetEmployeeExams([FromUri] BaseSearchModel model)
        {
            if (model == null)
                return BadRequest("No search params");

            model.ValidateSearchQuery();

            if (!_permissionService.TryCheckAccess(PermissionProvider.AccessLMS))
                return AccessDeniedResult();

            var examsDto = _userExaminationFactory.GetEmployeeAssignedExams(_workContext.User.UserId, model.Keywords, model.PageIndex, model.PageSize);

            var examsModel = examsDto.MapTo<IEnumerable<UserExamDto>, IEnumerable<UserExamModel>>();

            var result = new ApiResult<dynamic> { Result = examsModel };

            return Ok(result);
        }

        [HttpGet]
        [Route("GetExaminationSummary")]
        public IHttpActionResult GetExaminationSummary(int examinationId)
        {
            if (!_permissionService.TryCheckAccess(PermissionProvider.AccessLMS))
                return AccessDeniedResult();

            var examDto = _userExaminationFactory.GetExaminationSummary(examinationId, _workContext.User.UserId, _workContext.User.OrganizationId);

            if (examDto.IsNull()) {
                return NotFoundResult();
            }
            var examModel = examDto.MapTo<UserExamDto, UserExamModel>();
            var result = new ApiResult<dynamic>
            {
                Result = examModel
            };

            return Ok(result);
        }

        //public IHttpActionResult StartExamination(int examinationId)
        //{

        //    if (!_permissionService.TryCheckAccess(PermissionProvider.AccessLMS))
        //        return AccessDeniedResult();

        //    var exam = _userExaminationFactory.GetExaminationSummary(id);

        //    return Ok();
        //}

        [HttpGet]
        [Route("GetExaminationDetails")]
        public IHttpActionResult GetExaminationDetails(int examinationId)
        {
            if (!_permissionService.TryCheckAccess(PermissionProvider.AccessLMS))
                return AccessDeniedResult();

            return Ok();
        }
    }
}