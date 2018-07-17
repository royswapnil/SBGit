using Nop.Core.Caching;
using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Core.Helper;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Service;
using SterlingBankLMS.Data.UnitofWork;
using System.Collections.Generic;
using System.Linq;

namespace SterlingBankLMS.Core.Factories
{
    public class QuizQuestionFactory : GenericService<QuizQuestion>
    {
        public IEnumerable<QuizQuestion> GetLessonQuizzesAndOptions(int lessonId)
        {
            return GetAllIncluding(x => x.LessonId == lessonId, false, x => x.Options.Where(o => !o.IsDeleted)).ToList();
        }

        public QuizQuestion GetQuizAndOptions(int quizId)
        {
            return GetAllIncluding(x => x.Id == quizId && !x.IsDeleted, false, x => x.Options.Where(o => !o.IsDeleted)).FirstOrDefault();
        }

        private readonly ICacheManager _cacheManager;

        public IEnumerable<QuizQuestion> GetLessonQuizQuestionsAndOptions(LessonDto lesson)
        {
            var query = GetAllIncluding(x => x.LessonId == lesson.Id && !x.IsDeleted, false,
                                                x => x.Options.Where(o => !o.IsDeleted))
                                               .OrderBy(x => x.SortOrder);

            return query.ToList().PickRandomElements(lesson.QuestionNo);
        }

        public IEnumerable<QuizQuestion> GetLessonQuizQuestions(int lessonId, int? position = null)
        {
            var query = GetAllIncluding(x => x.LessonId == lessonId && !x.IsDeleted, false,
                                                x => x.Options.Where(o => !o.IsDeleted))
                                               .OrderBy(x => x.SortOrder);

            return position != null ? query.TakeWhile(x => x.SortOrder > position).ToList() : query.ToList();
        }

        public QuizQuestionFactory(IUnitOfWork unitOfWork, ICacheManager cacheManager) : base(unitOfWork)
        {
            _cacheManager = cacheManager;
        }
    }
}