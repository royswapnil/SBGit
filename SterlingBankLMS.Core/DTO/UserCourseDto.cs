using System;

namespace SterlingBankLMS.Core.DTO
{
    public class UserCourseDto
    {
        public int Id { get; set; }
        public int? CurrentLessonNumber { get; set; }
        public int? CurrentModuleNumber { get; set; }
    }

    public class UserCourseProgressDto
    {
        public int UserCourseId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CourseName { get; set; }
        public int? ExaminationScore { get; set; }
        public int TotalExamAttempts { get; set; }
        public int? QuizScore { get; set; }
        public int Completed { get; set; }
        public int LessonCounts { get; set; }
        public int TotalRecords { get; set; }
        public decimal CourseProgress { get; set; }
        public DateTime StartDate { get; set; }

        public string NameFormat => FirstName + ", " + LastName;
        public string StartDateFormat => StartDate.ToString("dd-MMM-yyyy");
        public string ProgressFormat => CourseProgress + "%";
    }

    public class UserExamProgressDto
    {
        public int UserExamId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ExamName { get; set; }
        public int? AverageScore { get; set; }
        public int? HighestScore { get; set; }
        public int Attempts { get; set; }
        public int ExamRetakeCount { get; set; }
        public int PassGrade { get; set; }
        public int TotalRecords { get; set; }
        public bool IsPassed { get; set; }
        public DateTime CreatedDate { get; set; }

        public string NameFormat => FirstName + ", " + LastName;
        public string StartDateFormat => CreatedDate.ToString("dd-MMM-yyyy");
    }
}
