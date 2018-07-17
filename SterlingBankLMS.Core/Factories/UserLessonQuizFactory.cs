using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Data.Models;
using SterlingBankLMS.Data.Service;
using SterlingBankLMS.Data.UnitofWork;
using System.Linq;

namespace SterlingBankLMS.Core.Factories
{
    public class UserLessonQuizFactory : GenericService<UserLessonQuiz>
    {
        public UserLessonQuizFactory(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public UserLessonQuiz FindCurrentUserQuizSession(int lessonId, int courseSessionId)
        {
            return Find(x => x.UserCourseId == courseSessionId
                          && x.LessonId == lessonId && !x.IsDeleted && !x.IsFinished, true);
        }

        public UserLessonQuiz FindUserQuizSession(int lessonId, int courseSessionId)
        {
            return Find(x => x.UserCourseId == courseSessionId
                          && x.LessonId == lessonId && !x.IsDeleted, true);
        }

        public int CountUserQuizSessionAttempts(int lessonId, int courseSessionId)
        {
            var query = from i in UnitOfWork.Repository<UserLessonQuiz>().Table
                        where i.UserCourseId == courseSessionId
                         && i.LessonId == lessonId && !i.IsDeleted
                         && i.IsFinished
                        orderby i.CreatedDate
                        select i;

            return query.Count();
        }

        public QuizSummaryScoreDto ComputeQuizSummary(int userCourseId, int lessonId)
        {
            return ExecuteProcedure<QuizSummaryScoreDto>("Sp_GetQuizSummaryScore", userCourseId, lessonId).FirstOrDefault();
        }
    }
}