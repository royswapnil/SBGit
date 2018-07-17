using SterlingBankLMS.Data.Models.Enums;
using System;

namespace SterlingBankLMS.Data.Models.Entities
{
    public class UserTraining: OrganizationalBaseEntity
    {
        public int UserId { get; set; }
        public int TrainingId { get; set; }
        public Training Training { get; set; }
        public User User { get; set; }
        public bool IsRequested { get; set; }
    }
}
