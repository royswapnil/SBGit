using MultipartDataMediaFormatter.Infrastructure;
using SterlingBankLMS.Data.Models.Enums;
using SterlingBankLMS.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SterlingBankLMS.Web.ViewModels
{
    public class AdvertModel : BaseModel
    {
        public string Title { get; set; }
        public AdvertSections Section { get; set; }
        public AdvertLocation Location { get; set; }
        public string LocationName => CommonHelper.GetDescription(this.Location);
        public string SectionName => CommonHelper.GetDescription(this.Section);
        public int? AdBannerId { get; set; }
        public string HTMLTag { get; set; }

        public string FileUrl { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string AdvertLink { get; set; }
        public bool IsActive { get; set; }
    }
}