using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Core.Helper;
using SterlingBankLMS.Data.Models;
using SterlingBankLMS.Data.Service;
using SterlingBankLMS.Data.UnitofWork;
using System.Collections.Generic;
using System.Linq;

namespace SterlingBankLMS.Core.Factories
{
    public class UserExaminationFactory : GenericService<UserExamination>
    {
        public UserExaminationFactory(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public UserExamDto GetAvailableExam(int courseId, int orgId)
        {
            var response = ExecuteProcedure<UserExamDto>("Sp_GetExamAndAttempt", courseId, orgId).FirstOrDefault();
            return response;
        }

        public UserExamination CreateNewUserExamination(int userId, UserExamDto exam)
        {
            var examSession = new UserExamination
            {
                Attempt = exam.ExamRetakeCount,
                ExaminationId = exam.ExaminationId,
                UserId = userId,
                CreatedById = userId,
                CreatedDate = AppHelper.GetCurrentDate()
            };

            Add(examSession);

            return examSession;
        }

        public IEnumerable<UserExamDto> GetEmployeeAssignedExams(int userId, string keywords, int pageIndex, int? pageSize)
        {
            return ExecuteProcedure<UserExamDto>("Sp_GetEmployeeAssignedExams", userId, keywords, pageIndex, pageSize, false).ToList();
        }

        public UserExamDto GetExaminationSummary(int id, int userId, int orgId)
        {
            return ExecuteProcedure<UserExamDto>("Sp_GetExaminationDetails", id, userId, orgId).FirstOrDefault();
        }

        //public ExaminationAttempt FindAttempt(int examId, int courseSessionId, bool track = false)
        //{
        //    var currDate = AppHelper.GetCurrentDate();
        //    return Find(x => x.ExaminationId == examId && !x.Completed && x.UserCourseId == courseSessionId
        //        && x.DueDate >= currDate, track);
        //}

        //public ExaminationAttempt StartNewAttemtpt(ClaUserExamDto exam, UserCourse userCourse)
        //{
        //    var startDate = AppHelper.GetCurrentDate();
        //    var endDate = startDate;

        //    if (exam.HourLimit.HasValue) {
        //        endDate = endDate.AddHours(exam.HourLimit.Value);
        //    }

        //    if (exam.MinuteLimit.HasValue) {
        //        endDate = endDate.AddMinutes(exam.MinuteLimit.Value);
        //    }

        //    var attempt = new ExaminationAttempt
        //    {
        //        DateStarted = startDate,
        //        DueDate = endDate,
        //        ExaminationId = exam.Id,
        //        UserId = userCourse.UserId,
        //        UserCourseId = userCourse.Id,
        //        CreatedDate = startDate,
        //        ModifiedDate = startDate,
        //        CreatedById = userCourse.UserId,
        //    };

        //    Add(attempt);

        //    return attempt;
        //}
    }
}