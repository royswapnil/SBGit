using SterlingBankLMS.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Core.DTO
{
    public class UserCertificateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TemplateUrl { get; set; }
        public DateTime DateObtained { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StaffId { get; set; }
        public string deptName { get; set; }
        public CertificateApprovalStatus CertificateApprovalStatus { get; set; }

        public string CertificateApprovalFormat => Enum.GetName(typeof(CertificateApprovalStatus), CertificateApprovalStatus);
        public string DateObtainedFormat => DateObtained.ToString("dd-MMM-yyyy");
        public string ExpiryDateFormat => ExpiryDate.ToString("dd-MMM-yyyy");
    }
}
