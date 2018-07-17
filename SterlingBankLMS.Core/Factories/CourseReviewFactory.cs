using System;
using SterlingBankLMS.Data.Models;
using SterlingBankLMS.Data.Service;
using SterlingBankLMS.Data.UnitofWork;
using SterlingBankLMS.Core.Helper;

namespace SterlingBankLMS.Core.Factories
{
    public class CourseReviewFactory : GenericService<CourseReview>
    {
        public CourseReviewFactory(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public CourseReview GetUserRatingForCourse(int userId, int courseId)
        {
            return Find(x => x.UserId == userId && x.CourseId == courseId && !x.IsDeleted, false);
        }

        public void CreateReview(CourseReview previousUserReview)
        {
            previousUserReview.CreatedById = previousUserReview.UserId;
            previousUserReview.ModifiedDate = previousUserReview.CreatedDate = AppHelper.GetCurrentDate();

            Add(previousUserReview);
        }
    }
}