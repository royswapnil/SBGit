using SterlingBankLMS.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SterlingBankLMS.Web.ViewModels
{
    public class SupportFAQPageModel
    {
        public AdvertDto TopSectionAd { get; set; }
        public List<FAQModel> FaqList { get; set; }
    }
}