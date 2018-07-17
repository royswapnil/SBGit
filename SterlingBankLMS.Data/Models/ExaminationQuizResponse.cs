using System;

namespace SterlingBankLMS.Data.Models.Entities
{
    public class ExaminationQuizResponse : TrackableEntity
    {
        public ExaminationAttempt ExaminationAttempt { get; set; }
        
        public int ExaminationAttemptId { get; set; }

        public ExaminationQuestion ExaminationQuestion { get; set; }

        public int ExaminationQuestionId { get; set; }

        public string Value { get; set; }

        public bool IsAnswer { get; set; }
    }
}