using System;
using SterlingBankLMS.Data.Models;
using SterlingBankLMS.Data.Service;
using SterlingBankLMS.Data.UnitofWork;

namespace SterlingBankLMS.Core.Factories
{
    public class CourseLikeFactory : GenericService<CourseLike>
    {
        public CourseLikeFactory(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public CourseLike FindUserLikeStatus(int courseId, int userId, bool track = false)
        {
            return Find(x => x.CourseId == courseId && !x.IsDeleted && x.UserId == userId, track);
        }

        public void CreateNewUserLike(CourseLike like)
        {
            Add(like);
        }
    }
}