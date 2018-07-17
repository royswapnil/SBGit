using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Service;
using SterlingBankLMS.Data.UnitofWork;
using System.Collections.Generic;
using System.Linq;
using System;

namespace SterlingBankLMS.Core.Factories
{
    public class ExaminationQuestionFactory : GenericService<ExaminationQuestion>
    {
        public ExaminationQuestionFactory(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<ExaminationQuestion> GetExaminationQuestions(int examId)
        {
            return GetAllIncluding(x => x.ExaminationId == examId && !x.IsDeleted, false,
                x => x.Options.Where(o => !o.IsDeleted))
                .OrderBy(x => x.SortOrder).ToList();
        }

        public ExaminationQuestion GetQuestionAndOptions(int questionId)
        {
            return GetIncluding(x => x.Id == questionId && !x.IsDeleted, false,
              x => x.Options.Where(o => !o.IsDeleted));
        }
    }
}