using System;
using System.Collections.Generic;

namespace SterlingBankLMS.Data.Models.Entities
{
    public class ExaminationAttempt: TrackableEntity
    {
        public UserExamination UserExmanination { get; set; }

        public int UserExmaninationId { get; set; }

        public Examination Examination { get; set; }

        public int? ExaminationId { get; set; }

        public DateTime? DueDate { get; set; }
        public DateTime DateStarted { get; set; }
        public DateTime? DateCompleted { get; set; }

        public bool Completed { get; set; }

        public bool? IsPassed { get; set; }

        public decimal Score { get; set; }

        public ICollection<ExaminationQuizResponse> Response { get; set; }
    }
}