using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Core.Factories;
using SterlingBankLMS.Web.Infrastructure.Services;
using SterlingBankLMS.Web.Utilities;
using SterlingBankLMS.Web.Utilities.Extensions;
using SterlingBankLMS.Web.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SterlingBankLMS.Web.Api
{
    /// <summary>
    /// Employee dashboard api
    /// </summary>
    [RoutePrefix("api/EmployeeDashboard")]
    public class EmployeeDashboardController : BaseApiController
    {
        private readonly UserFactory _employeeFactory;
        private readonly CourseFactory _courseFactory;
        private readonly LearningAreaFactory _LearningAreaFactory;
        private readonly IWorkContext _workContext;
        private readonly IPermissionService _permissionSvc;

        public EmployeeDashboardController(
            CourseFactory courseFactory, LearningAreaFactory LearningAreaFactory,
            IWorkContext workContext, IPermissionService permissionSvc,
            UserFactory employeeFactory)
        {
            _courseFactory = courseFactory;
            _LearningAreaFactory = LearningAreaFactory;
            _workContext = workContext;
            _permissionSvc = permissionSvc;
            _employeeFactory = employeeFactory;
        }
        /// <summary>
        /// Get Employee top 3 assined and recommended courses
        /// </summary>
        /// <returns></returns>
        [Route("GetEmployeeCourses")]
        public IHttpActionResult GetAssignedCourses()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.AccessLMS)) {
                return AccessDeniedResult();
            }

            var assignedCoursesDto = _courseFactory.GetRequiredCourses(_workContext.User.UserId, _workContext.User.OrganizationId, 1, 3, out int assignedCount);
            var assignedCourses = assignedCoursesDto.MapTo<IEnumerable<AssignedCourseDto>, IEnumerable<AssignedCourseModel>>();

            var recommendedCoursesDto = _courseFactory.GetRecommenderCourses(_workContext.User.UserId, _workContext.User.OrganizationId, 1, 3, out int recommendedCount);
            var recommendedCourses = recommendedCoursesDto.MapTo<IEnumerable<AssignedCourseDto>, IEnumerable<AssignedCourseModel>>();

            var model = new EmployeeHomePageModel
            {
                AssignedCourses = assignedCourses,
                AssignedCoursesHasMore = CollectionExtensions.HasMorePage(assignedCount, 1, 3),
                RecommendedCourses = recommendedCourses,
                RecommendedCoursesHasMore = CollectionExtensions.HasMorePage(recommendedCount, 1, 3),
            };

            var result = new ApiResult<EmployeeHomePageModel> { Result = model };

            return Ok(result);
        }

        /// <summary>
        /// Get Employee assigned courses
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [Route("GetMoreAssignedCourse")]
        public IHttpActionResult GetMoreAssignedCourses(int page)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.AccessLMS)) {
                return AccessDeniedResult();
            }

            var assignedCoursesDto = _courseFactory.GetRequiredCourses(_workContext.User.UserId, _workContext.User.OrganizationId, page, 3, out int assignedCount);
            var assignedCourses = assignedCoursesDto.MapTo<IEnumerable<AssignedCourseDto>, IEnumerable<AssignedCourseModel>>();

            var model = new EmployeeHomePageModel
            {
                AssignedCourses = assignedCourses,
                AssignedCoursesHasMore = CollectionExtensions.HasMorePage(assignedCount, page, 3),
            };

            var result = new ApiResult<EmployeeHomePageModel> { Result = model };

            return Ok(result);
        }

        /// <summary>
        /// Get Employee recommended courses
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [Route("GetMoreRecommendedCourse")]
        public IHttpActionResult GetMoreRecommendedCourses(int page)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.AccessLMS)) {
                return AccessDeniedResult();
            }

            var recommendedCoursesDto = _courseFactory.GetRecommenderCourses(_workContext.User.UserId, _workContext.User.OrganizationId, page, 3, out int recommendedCount);
            var recommendedCourses = recommendedCoursesDto.MapTo<IEnumerable<AssignedCourseDto>, IEnumerable<AssignedCourseModel>>();

            var model = new EmployeeHomePageModel
            {
                AssignedCourses = recommendedCourses,
                AssignedCoursesHasMore = CollectionExtensions.HasMorePage(recommendedCount, page, 3),
            };

            var result = new ApiResult<EmployeeHomePageModel> { Result = model };

            return Ok(result);
        }

        /// <summary>
        /// Get employee top 3 course learning progress
        /// </summary>
        /// <returns></returns>
        [Route("GetLearningProgress")]
        public IHttpActionResult GetLearningProgress()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.AccessLMS)) {
                return AccessDeniedResult();
            }

            var courseProgressDto = _courseFactory.GetLearningProgress(_workContext.User.UserId, _workContext.User.OrganizationId);
            var courseProgress = courseProgressDto.MapTo<IEnumerable<CourseProgressDto>, IEnumerable<CourseProgressModel>>();

            var result = new ApiResult<CollectionResult<CourseProgressModel>>
            {
                Result = new CollectionResult<CourseProgressModel>(courseProgress)
                {
                    Total = courseProgress.Count()
                }
            };

            return Ok(result);
        }
    }
}