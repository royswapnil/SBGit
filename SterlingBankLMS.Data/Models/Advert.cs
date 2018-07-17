using SterlingBankLMS.Data.Models.Enums;
using System;
using System.Collections.Generic;

namespace SterlingBankLMS.Data.Models.Entities
{
    public class Advert: OrganizationalBaseEntity
    {
        public string Title { get; set; }

        public AdBanner AdBanner { get; set; }

        public int? AdBannerId { get; set; }
        public string HTMLTag { get; set; }
        public DateTime StartDate { get; set; }
        public AdvertSections Section { get; set; }
        public AdvertLocation Location { get; set; }
        public DateTime EndDate { get; set; }
        public string AdvertLink { get; set; }
        public bool IsActive { get; set; }
    }
































}