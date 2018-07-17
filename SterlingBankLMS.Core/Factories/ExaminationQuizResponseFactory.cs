using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Service;
using SterlingBankLMS.Data.UnitofWork;
using System.Linq;
using System;
using System.Collections.Generic;

namespace SterlingBankLMS.Core.Factories
{
    public class ExaminationQuizResponseFactory : GenericService<ExaminationQuizResponse>
    {
        public ExaminationQuizResponseFactory(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public ExaminationResponseDto GetCurrentSessionLastAttemptAnswer(int examId, int userId)
        {
            return ExecuteProcedure<ExaminationResponseDto>("Sp_GetLastExamResponse", examId, userId).FirstOrDefault();
        }

        public ExaminationQuizResponse FindQuestionResponseForAttempt(int id, int questionId)
        {
            return All(x => x.ExaminationAttemptId == id && x.ExaminationQuestionId == questionId && !x.IsDeleted, false).FirstOrDefault();
        }

        public List<ExaminationQuizResponse> FindResponsesForSession(int id, int questionId)
        {
            return All(x => x.ExaminationAttemptId == id && x.ExaminationQuestionId == questionId && !x.IsDeleted, false);
        }
    }
}