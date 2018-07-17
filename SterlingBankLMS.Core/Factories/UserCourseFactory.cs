using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Core.Helper;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Models.Enums;
using SterlingBankLMS.Data.Service;
using SterlingBankLMS.Data.UnitofWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Z.EntityFramework.Plus;

namespace SterlingBankLMS.Core.Factories
{
    public class UserCourseFactory : GenericService<UserCourse>
    {
        private readonly CourseFactory _courseFactory;
        private readonly ModuleFactory _moduleFactory;
        public UserCourseFactory(IUnitOfWork unitOfWork, CourseFactory courseRepository,
            ModuleFactory moduleFactory)
            : base(unitOfWork)
        {
            _courseFactory = courseRepository;
            _moduleFactory = moduleFactory;
        }

        public UserCourse CreateNewSession(int userId, int courseId, int orgId)
        {
            var session = new UserCourse
            {
                CourseId = courseId,
                UserId = userId,
                CreatedById = userId,
                CourseStatus = UserCourseStatus.Incomplete,
            };

            session.StartDate = session.CreatedDate = AppHelper.GetCurrentDate();

            var assignedCourse = _courseFactory
                                            .GetAllAssignedCoursesForUser(userId, orgId)
                                            .Where(c => c.Id == courseId).FirstOrDefault();

            if (assignedCourse != null) {

                session.Availability = session.Availability;
                session.LearningGroupId = assignedCourse.LearningGroupId;
            }
            else {
                session.Availability = UserCourseAvailability.Requested;
            }

            Add(session);

            return session;
        }

        public List<CertificateDto> GetPagedUserCertificate(int UserId, int pageSize, int pageNumber, out int TotalRecords)
        {

            TotalRecords = pageNumber;
            var _userCourseContext = UnitOfWork.Repository<UserCourse>().TableNoTracking;
            var _courseContext = UnitOfWork.Repository<Course>().TableNoTracking;


            var query = (from x in _userCourseContext
                         join z in _courseContext
                               on x.CourseId equals z.Id
                         where x.Completed && z.HasCertificate && !x.IsDeleted && x.UserId == UserId

                         select new CertificateDto
                         {
                             Id = (int)x.Id,
                             Name = z.Name,
                             CourseId = x.CourseId,
                             DateStarted = x.CreatedDate,
                             DateCompleted = x.CertificateDate,
                             DueDate = z.DueDate
                         });

            var futureList = query.OrderBy(x => x.Id).Skip((pageNumber - 1) * pageSize).Take(pageSize).Future();

            if (pageNumber == 1)
            {
                var countQuery = (from x in _userCourseContext
                                  join z in _courseContext
                              on x.CourseId equals z.Id
                                  where x.Completed && z.HasCertificate && !x.IsDeleted && x.UserId == UserId
                                  select x.Id
                           ).DeferredCount().FutureValue();
                TotalRecords = countQuery.Value;
            }

            var ReturnList = futureList.ToList();

            return ReturnList;

        }

        
        public UserCourse GetCertificate(int userCourseId)
        {
          
            var userCourse = this.GetIncluding(x => x.Id == userCourseId, true, x => x.Course);

            if (userCourse == null)
                return null;


            if (!userCourse.CertificateDownloaded)
            {
                userCourse.CertificateDownloaded = true;
                userCourse.CertificateDate = AppHelper.GetCurrentDate();
                Update(userCourse);
            }

            return userCourse;

        }

        public UserCourseCompletedLessonDto GetCourseSessionCompletedLession(int userCourseId)
        {
            return ExecuteProcedure<UserCourseCompletedLessonDto>("Sp_GetSessionCompletedLesson", userCourseId).FirstOrDefault();
        }
    }
}