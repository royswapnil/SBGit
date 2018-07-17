using Nop.Core.Caching;
using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Core.Helper;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Models.Enums;
using SterlingBankLMS.Data.Service;
using SterlingBankLMS.Data.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using Z.EntityFramework.Plus;

namespace SterlingBankLMS.Core.Factories
{
    public class CourseFactory : GenericService<Course>
    {
        private readonly ICacheManager _cacheManagerSvc;

        public readonly string ASSIGNEDCOURSESORG = "assignedcourses.{0}.";
        public readonly string USER = "{1}";
        public readonly string COURSEDETAILS = "coursedetails-{0}-{1}";
        public readonly string COURSEPROGRESS = "courseprogress-{0}-{1}";

        public CourseFactory(IUnitOfWork unitOfWork, ICacheManager cacheManagerSvc) : base(unitOfWork)
        {
            _cacheManagerSvc = cacheManagerSvc;
        }

        public UserCourseStatusDto HasSurveyOrReview(int courseId, int userId, int organizationId)
        {
            return ExecuteProcedure<UserCourseStatusDto>("Sp_HasTakenSurveyorReview", courseId, userId, organizationId).FirstOrDefault();
        }

        public IEnumerable<CourseDto> GetCoursesTable(string search, int pageIndex, int pageSize, int organizationId)
        {
            return ExecuteProcedure<CourseDto>("Sp_GetCoursesList", organizationId, search, pageIndex, pageSize).ToList();
        }

        public void ClearCachedUserCourseProgress(int userId, int orgId)
        {
            var key = string.Format(COURSEPROGRESS, userId, orgId);
            _cacheManagerSvc.Remove(key);
        }

        public void ClearAllCachedAssignedCourses(int orgId)
        {
            var key = string.Format(ASSIGNEDCOURSESORG, orgId);
            _cacheManagerSvc.RemoveByPattern(key);
        }

        public void ClearCachedUserAssignedCourses(int userId, int orgId)
        {
            var key = string.Format($"{ASSIGNEDCOURSESORG}{USER}", orgId, userId);
            _cacheManagerSvc.Remove(key);
        }

        public IEnumerable<CatalogCourseDto> SearchCourses(
            string keyWords, int? filter, int? filterBy,
            int organizationId, int pageIndex, int? pageSize)
        {
            return ExecuteProcedure<CatalogCourseDto>("SP_SearchCourses",
                keyWords, filter, filterBy, organizationId,
                pageIndex, pageSize, false).ToList();
        }

        public IEnumerable<CourseProgressDto> GetUserCourseProgress(int userId, int orgId)
        {
            return ExecuteProcedure<CourseProgressDto>("SP_GetEmployeeCourseProgress",
                                                           userId, orgId, false).ToList();
        }


        public bool ValidateClaCourse(int courseId, int? moduleId, int? lessonId, int orgId)
        {
            return ExecuteProcedure<CourseProgressDto>("Sp_ValidateCLaCourse",
                                                         courseId, moduleId, lessonId, orgId)
               .FirstOrDefault() != null;
        }

        public UserCourse GetCurrentCourseSession(int courseId, int userId)
        {
            var courseSession = from session in UnitOfWork.Repository<UserCourse>().Table
                                where !session.IsDeleted && session.UserId == userId && session.CourseId == courseId
                                && session.CourseStatus == UserCourseStatus.Incomplete
                                select session;

            return courseSession.FirstOrDefault();
        }

        public ClaCourseDto GetClaCourseDetails(int courseId, int orgId)
        {
            var currentDate = AppHelper.GetCurrentDate();

            var query = from c in UnitOfWork.Repository<Course>().TableNoTracking
                        where !c.IsDeleted && c.OrganizationId == orgId
                        && courseId == c.Id && c.IsPublished && (!c.DueDate.HasValue || c.DueDate.Value >= currentDate)
                        select new ClaCourseDto { Id = c.Id, Name = c.Name, };

            return query.FirstOrDefault();
        }

        public IEnumerable<CourseProgressDto> GetLearningProgress(int userId, int orgId)
        {
            string key = string.Format(COURSEPROGRESS, userId, orgId);

            return _cacheManagerSvc.Get(key, () => {
                return ExecuteProcedure<CourseProgressDto>("Sp_GetEmployeeCourseProgress", userId, orgId, false).Take(3).ToList();
            });
        }

        public IEnumerable<AssignedCourseDto> GetRequiredCourses(int userId, int orgId, int page, int size, out int totalCount)
        {
            var totalRecords = GetAllAssignedCoursesForUser(userId, orgId)
                 .Where(x => x.Availability == UserCourseAvailability.Required);

            totalCount = totalRecords.Count();

            return totalRecords.Skip((page - 1) * size).Take(size);
        }

        public IEnumerable<AssignedCourseDto> GetRecommenderCourses(int userId, int orgId, int page, int size, out int totalCount)
        {
            var totalRecords = GetAllAssignedCoursesForUser(userId, orgId)
                .Where(x => x.Availability == UserCourseAvailability.Generic);

            totalCount = totalRecords.Count();

            return totalRecords.Skip((page - 1) * size).Take(size);
        }

        public IEnumerable<AssignedCourseDto> GetAllAssignedCoursesForUser(int userId, int orgId)
        {
            string key = string.Format($"{ASSIGNEDCOURSESORG}{USER}", orgId, userId);
            var totalRecords = _cacheManagerSvc.Get(key, () => {
                return ExecuteProcedure<AssignedCourseDto>("Sp_GetEmployeeAssignedCourses", userId, orgId, null, false)
                .ToList()
                .Distinct(new AssignedCourseDtoComparer());
            });

            return totalRecords;
        }

        public CourseDetailDto GetCourseDetails(int courseId, int orgId)
        {
            string key = string.Format(COURSEDETAILS, orgId, courseId);

            return _cacheManagerSvc.Get(key, () => {
                return ExecuteProcedure<CourseDetailDto>("Sp_GetCourseDetails", courseId, orgId, false).FirstOrDefault();
            });
        }

        public void ClearCachedCourse(int orgId,int courseId)
        {
            var key = string.Format(COURSEDETAILS, orgId, courseId);
            _cacheManagerSvc.RemoveByPattern(key);
        }

        public IEnumerable<CourseDto> SearchCourseResults(string search)
        {
            var context = this.GetContext();

            var results = context.Set<Course>().Where(x => x.Name.ToLower().Contains(search.ToLower()) || x.LearningArea.Name.ToLower().Contains(search.ToLower())).Select(x => new CourseDto
            {
                Id = x.Id,
                LearningAreaName = x.LearningArea.Name,
                LearningAreaId = x.LearningAreaId,
                DueDate = x.DueDate,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                Name = x.Name,

            }).ToList();


            return results;
        }


        public bool ValidateCourseNameExists(string Name,int Id)
        {
            var existingCourse = Find(x => x.Name == Name && x.Id != Id, false);
            return (existingCourse == null);

        }

        /// <summary>
        /// Check if course has assigned users and if it has started to stop delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int ValidateCourseForDelete(int id)
        {
            var startDate = AppHelper.GetCurrentDate();

            var courseContext = GetContext().Set<Course>();
            var UserCourseContext = GetContext().Set<UserCourse>();

            var UserCourseCount = (from c in courseContext
                                   join uc in UserCourseContext
                                   on c.Id equals uc.CourseId
                                   where c.Id == id && uc.StartDate > startDate && !uc.IsDeleted
                                   select c).Count();
            return UserCourseCount;
        }

        public void CloneCourse(int CourseId,string Name, int UserId)
        {
            var course = Find(x=>x.Id == CourseId && x.IsDeleted == false,false);
            if(course == null)
                throw new ArgumentNullException("Course not found");

            ExecuteProcedure<CourseDetailDto>("SP_CLONECOURSE", CourseId, Name, UserId);         
        }
    }
}