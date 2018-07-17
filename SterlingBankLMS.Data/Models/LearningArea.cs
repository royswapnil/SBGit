using System.Collections.Generic;

namespace SterlingBankLMS.Data.Models.Entities
{
    public class LearningArea: OrganizationalBaseEntity
    {
        public string Name { get; set; }

        public ICollection<Course> Courses { get; set; }
    }
}
