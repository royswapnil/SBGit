namespace SterlingBankLMS.Core.DTO
{
    public class UserExamDto
    {
        public int ExaminationId { get; set; }
        public decimal? PassGrade { get; set; }
        public int ExamRetakeCount { get; set; }
        public int UserAttemptCounts { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public double? HourLimit { get; set; }
        public double? MinuteLimit { get; set; }

        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int TotalCount { get; set; }
    }

    public static class UserExamDtoExtensions
    {
        public static bool IsNull( this UserExamDto userExam)
        {
            return userExam == null;
        }
    }
}