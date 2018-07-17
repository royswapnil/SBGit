namespace SterlingBankLMS.Core.DTO
{
    public class QuizSummaryScoreDto
    {
        public int? Questions { get; set; }
        public int? QuestionNo { get; set; }
        public int? MaxScore { get; set; }
        public int? LowScore { get; set; }
        public int? Score { get; set; }
        public int TotalAttempts { get; set; }
        public int Retake { get; set; }
        public decimal? PassMark { get; set; }
    }
}