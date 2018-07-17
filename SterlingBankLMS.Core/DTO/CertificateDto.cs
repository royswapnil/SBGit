using SterlingBankLMS.Data.Models;
using SterlingBankLMS.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Core.DTO
{
   public class CertificateDto : BaseEntity
    {
        
        public string Name { get; set; }

        public int CourseId { get; set; }

        public DateTime? DateCompleted { get; set; }

        public DateTime DateStarted { get; set; }

        public string DateCompletedFormat => Convert.ToDateTime(DateCompleted).ToString("dd MMM, yyyy");

        public string DateStartedFormat => DateStarted.ToString("dd MMM, yyyy");

        public DateTime? DueDate { get; set; }

        public string DueDateFormat => DueDate == null ? "N/A" : Convert.ToDateTime(DueDate).ToString("dd MMM, yyyy");
    }
}
