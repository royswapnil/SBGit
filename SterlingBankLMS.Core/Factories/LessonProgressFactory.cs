using System;
using SterlingBankLMS.Data.Models;
using SterlingBankLMS.Data.Service;
using SterlingBankLMS.Data.UnitofWork;

namespace SterlingBankLMS.Core.Factories
{
    public class LessonProgressFactory : GenericService<LessonProgress>
    {
        public LessonProgressFactory(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public LessonProgress GetSessionLessonProgress(int lessonId, int moduleId, int userCourseId)
        {
            return Find(x => x.ModuleId == moduleId && x.LessonId == lessonId
                           && x.UserCourseId == userCourseId && !x.IsDeleted,
                       true);
        }

    }
}