namespace SterlingBankLMS.Core.DTO
{
    public class QuizResponseDto
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public bool IsAnswer { get; set; }
        public int QuizId { get; set; }
        public int SortOrder { get; set; }
    }

    public static class QuizResponseDtoExtions {

        public static bool IsNull(this QuizResponseDto quizResponse)
        {
            return quizResponse == null;
        }
    }
}