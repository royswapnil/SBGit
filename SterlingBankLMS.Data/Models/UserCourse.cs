using SterlingBankLMS.Data.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace SterlingBankLMS.Data.Models.Entities
{
    public class UserCourse : TrackableEntity
    {
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public int? CertificateId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public bool Completed { get; set; }

        public UserCourseAvailability Availability { get; set; }
        public UserCourseStatus CourseStatus { get; set; }
        public int? CurrentModuleNumber { get; set; }
        public int? CurrentLessonNumber { get; set; }
        public bool CertificateDownloaded { get; set; }
        public DateTime? CertificateDate { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int? LearningGroupId { get; set; }
        public LearningGroup LearningGroup { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public decimal? Score { get; set; }
        public string Name { get; set; }
    }

    public static class UserCourseExtensions
    {
        public static bool IsNull(this UserCourse userCourse)
        {
            return userCourse == null;
        }
    }
}