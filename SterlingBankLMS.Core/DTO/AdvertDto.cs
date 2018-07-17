using SterlingBankLMS.Core.Helper;
using SterlingBankLMS.Data.Models.Enums;
using System;
using System.Collections.Generic;

namespace SterlingBankLMS.Core.DTO
{
    public class AdvertDto

    {
        public int Id { get; set; }
        public int? AdBannerId { get; set; }
        public string Title { get; set; }
        public string FileUrl { get; set; }
        public string HTMLTag { get; set; }
        public AdvertSections Section { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string AdvertLink { get; set; }
        public bool IsActive { get; set; }
        public AdvertLocation Location { get; set; }
    }

}
