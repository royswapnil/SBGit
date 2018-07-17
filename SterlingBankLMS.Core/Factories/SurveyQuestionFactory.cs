using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Service;
using SterlingBankLMS.Data.UnitofWork;
using System.Collections.Generic;
using System.Linq;

namespace SterlingBankLMS.Core.Factories
{
    public class SurveyQuestionFactory : GenericService<SurveyQuestion>
    {
        public SurveyQuestionFactory(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<SurveyQuestion> GetSurveyQuestions(int courseId, int orgId, out int? surveyId)
        {
            surveyId = null;

            var survey = UnitOfWork.Repository<Survey>().Fetch(x => x.CourseId == courseId && !x.IsDeleted, false).FirstOrDefault();

            if (survey == null) {
                return new List<SurveyQuestion>();
            }

            surveyId = survey.Id;

            return GetAllIncluding(x => x.TemplateId == survey.TemplateId, false, x => x.Options.Where(q => !q.IsDeleted))
                .OrderBy(y => y.SortOrder);
        }

        public SurveyQuestion GetsQuestionWithOptions(int questionId, int orgId)
        {
            var question = from q in UnitOfWork.Repository<SurveyQuestion>().TableNoTracking
                           where q.Id == questionId && q.Template.OrganizationId == orgId
                           select q;

            return GetIncluding(x => x.Id == questionId && !x.IsDeleted, false, x => x.Options);
        }
    }
}