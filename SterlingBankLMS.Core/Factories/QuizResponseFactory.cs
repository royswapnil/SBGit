using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Service;
using SterlingBankLMS.Data.UnitofWork;
using System.Linq;
using System;
using System.Collections.Generic;

namespace SterlingBankLMS.Core.Factories
{
    public class QuizResponseFactory : GenericService<QuizResponse>
    {
        public QuizResponseFactory(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public QuizResponseDto GetCurrentQuizSessionLastAttemptAnswer(int lessonId, int userId)
        {
            return ExecuteProcedure<QuizResponseDto>("Sp_GetLastQuizResponse", lessonId, userId).FirstOrDefault();
        }

        //Todo: change this to Linq
        public QuizResponse FindQuizResponseForSession(int userQuizSessionId, int quizId)
        {
            return All(x => x.UserLessonQuizId == userQuizSessionId && x.QuizId == quizId && !x.IsDeleted, false).FirstOrDefault();
        }

        public List<QuizResponse> FindQuizResponsesForSession(int userQuizSessionId, int quizId)
        {
            return All(x => x.UserLessonQuizId == userQuizSessionId && x.QuizId == quizId && !x.IsDeleted, false);
        }
    }
}