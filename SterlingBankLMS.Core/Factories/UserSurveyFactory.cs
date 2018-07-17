using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Service;
using SterlingBankLMS.Data.UnitofWork;

namespace SterlingBankLMS.Core.Factories
{
    public class UserSurveyFactory : GenericService<UserSurvey>
    {
        public UserSurveyFactory(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public UserSurvey FindUserSurveySession(int surveyId, int userId, bool track = false)
        {
            return Find(x => x.SurveyId == surveyId && x.UserId == userId && !x.IsDeleted, track);
        }
    }
}