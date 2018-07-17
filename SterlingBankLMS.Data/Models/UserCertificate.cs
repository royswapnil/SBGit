using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Data.Models
{
    public class UserCertificate : OrganizationalBaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string TemplateUrl { get; set; }

        public DateTime DateObtained { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }
        public CertificateApprovalStatus CertificateApprovalStatus { get; set; }
    }
}
 