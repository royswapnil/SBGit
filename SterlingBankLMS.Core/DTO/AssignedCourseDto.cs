using SterlingBankLMS.Data.Models.Enums;
using System.Collections.Generic;

namespace SterlingBankLMS.Core.DTO
{
    public class AssignedCourseDto
    {
        public int Id { get; set; }
        public double? AverageRating { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string ImageUrl { get; set; }
        public string DueDate { get; set; }
        public string LearningArea { get; set; }
        public int LearningGroupId { get; set; }
        public UserCourseAvailability? Availability { get; set; }
    }

    public class AssignedCourseDtoComparer : IEqualityComparer<AssignedCourseDto>
    {
        public bool Equals(AssignedCourseDto x, AssignedCourseDto y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return false;

            if (x.Id == y.Id) {
                return true;
            }

            return false;
        }

        public int GetHashCode(AssignedCourseDto member)
        {
            int hashX = member.Id.GetHashCode();
            int hashY = member.LearningGroupId.GetHashCode();
            int hashZ = member.Availability.GetHashCode();

            return hashX ^ hashY ^ hashZ;
        }
    }
}