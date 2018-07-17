using SterlingBankLMS.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SterlingBankLMS.Web.ViewModels
{
    public class EmployeeSideBarModel
    {
      
        public AdvertDto SideSectionAd { get; set; }

        public List<CertificateDto> Certificates { get; set; }

        public int totalCertificates { get; set; }

        public List<UpcomingTrainingModel> UpcomingTraining { get; set; }
    }
}