using SterlingBankLMS.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Web.ViewModels
{
    public class CertificateUploadModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TemplateUrl { get; set; }
        public int UserId { get; set; }
        public DateTime DateObtained { get; set; }
        public DateTime ExpiryDate { get; set; }
        public CertificateApprovalStatus ApprovalStatus { get; set; }
    }
}
