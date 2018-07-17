using SterlingBankLMS.Data.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace SterlingBankLMS.Data.Models.Entities
{
    public class UserSurvey : TrackableEntity
    {
        public int SurveyId { get; set; }
        public Survey Survey { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public bool IsFinished { get; set; }
    }
}