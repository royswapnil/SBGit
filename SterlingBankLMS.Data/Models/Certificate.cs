using SterlingBankLMS.Data.Models.Enums;
using System;

namespace SterlingBankLMS.Data.Models.Entities
{
    public class Certificate: OrganizationalBaseEntity
    {
        public Course Course { get; set; }
        public int CourseId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string TemplateUrl { get; set; }

        
    }
}
