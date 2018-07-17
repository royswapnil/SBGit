using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Models.Enums;
using System;

namespace SterlingBankLMS.Data.Models.Entities
{
    public class TrainingRequest : OrganizationalBaseEntity
    {
        public Training Training { get; set; }
        public int TrainingId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public User LineManagerActionBy { get; set; }

        public User AdminActionBy { get; set; }
        public int? LineManagerActionById { get; set; }
        public int? AdminActionById { get; set; }

        public string LineManagerComment {get;set;}

        public string AdminComment { get; set; }

        public DateTime? LineManagerActionDate { get; set; }

        public DateTime? AdminActionDate { get; set; }

        public string ReasonForRequest { get; set; }
        public TrainingApprovalStatus TrainingApprovalStatus { get; set; }

    }
}
